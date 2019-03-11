using DotvvmAcademy.Validation;
using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Dothtml;
using DotvvmAcademy.Validation.Dothtml.Unit;
using DotvvmAcademy.Validation.Unit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.Loader;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat.Sandbox
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //Debugger.Launch();

            // extract args
            if (args.Length < 3)
            {
                throw new InvalidOperationException("The sandbox needs to be run with at least 3 arguments: " +
                    "name of the memory mapped file with the script assembly, " +
                    "name of the entry type inside the script assembly, " +
                    "name of the entry method inside the entry type.");
            }

            // load Unit
            IValidationUnit unit;
            using (var scriptMap = MemoryMappedFile.OpenExisting(args[0], MemoryMappedFileRights.Read))
            using (var mapStream = scriptMap.CreateViewStream(0, 0, MemoryMappedFileAccess.Read))
            {
                var scriptAssembly = AssemblyLoadContext.Default.LoadFromStream(mapStream);
                var submissionType = scriptAssembly.GetType(args[1]);
                var factoryMethod = submissionType.GetMethod(args[2]);
                var unitProperty = submissionType.GetProperty("Unit");
                var submissionArray = new object[2]; // global object and Submission#0
                await (Task<object>)factoryMethod.Invoke(null, new object[] { submissionArray });
                unit = (IValidationUnit)unitProperty.GetValue(submissionArray[1]);
            }

            // load dependencies
            var sources = new List<ISourceCode>();
            for (int i = 3; i < args.Length; i++)
            {
                var mapName = args[i];

                // read contents
                string text;
                using (var map = MemoryMappedFile.OpenExisting(mapName, MemoryMappedFileRights.Read))
                using (var mapStream = map.CreateViewStream(0, 0, MemoryMappedFileAccess.Read))
                using (var reader = new StreamReader(mapStream))
                {
                    text = await reader.ReadToEndAsync();
                    text = text.TrimEnd('\0');
                }

                if (mapName.EndsWith(".cs"))
                {
                    sources.Add(new CSharpSourceCode(text, mapName, false));
                }
                else if (mapName.EndsWith(".dothtml"))
                {
                    sources.Add(new DothtmlSourceCode(text, mapName, false));
                }
                else
                {
                    throw new InvalidOperationException($"Dependency '{mapName}' is of unknown type.");
                }
            }

            // read validated code
            var validatedSource = await Console.In.ReadToEndAsync();

            // pick validation service and validation source code type
            switch (unit)
            {
                case CSharpUnit _:
                    sources.Add(new CSharpSourceCode(validatedSource, "UserCode.cs", true));
                    break;

                case DothtmlUnit _:
                    sources.Add(new DothtmlSourceCode(validatedSource, "UserCode.dothtml", true));
                    break;

                default:
                    throw new InvalidOperationException($"Validation unit of type '{unit.GetType()}' is not supported.");
            }

            // validate and write out diagnostics
            var validationService = new ValidationService();
            var diagnostics = await validationService.Validate(unit.GetConstraints(), sources);
            var lightDiagnostics = diagnostics.Select(d => new LightDiagnostic(d))
                .ToArray();
            var formatter = new BinaryFormatter();
            using (var outStream = Console.OpenStandardOutput())
            {
                formatter.Serialize(outStream, lightDiagnostics);
            }
        }

        private static byte[] UnmapByteArray(string mapName)
        {
            using (var mmf = MemoryMappedFile.OpenExisting(mapName, MemoryMappedFileRights.Read))
            using (var view = mmf.CreateViewAccessor(0, 0, MemoryMappedFileAccess.Read))
            {
                var length = view.ReadInt32(0);
                var array = new byte[length];
                if (view.ReadArray(sizeof(int), array, 0, length) != length)
                {
                    throw new InvalidOperationException("Couldn't read the array.");
                }
                return array;
            }
        }
    }
}