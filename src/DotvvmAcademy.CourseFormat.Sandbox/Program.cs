using DotvvmAcademy.Validation;
using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Dothtml;
using DotvvmAcademy.Validation.Dothtml.Unit;
using DotvvmAcademy.Validation.Unit;
using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Reflection;
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
            string scriptAssemblyName = args[0];
            string scriptEntryType = args[1];
            string scriptEntryMethod = args[2];
            if (args.Length < 3)
            {
                throw new InvalidOperationException("The sandbox needs to be run with at least 3 arguments: " +
                    "name of the memory mapped file with the script assembly, " +
                    "name of the entry type inside the script assembly, " +
                    "name of the entry method inside the entry type.");
            }

            // load Unit
            Assembly codeTaskAssembly;
            using (var codeTaskFile = MemoryMappedFile.OpenExisting(scriptAssemblyName, MemoryMappedFileRights.Read))
            using (var stream = codeTaskFile.CreateViewStream(0, 0, MemoryMappedFileAccess.Read))
            {
                codeTaskAssembly = AssemblyLoadContext.Default.LoadFromStream(stream);
            }
            var submissionType = codeTaskAssembly.GetType(scriptEntryType);
            var factoryMethod = submissionType.GetMethod(scriptEntryMethod);
            var unitProperty = submissionType.GetProperty("Unit");
            var submissionArray = new object[2]; // global object and Submission#0
            await (Task<object>)factoryMethod.Invoke(null, new object[] { submissionArray });
            var unit = (IValidationUnit)unitProperty.GetValue(submissionArray[1]);

            // load dependencies
            var sources = ImmutableArray.CreateBuilder<ISourceCode>();
            for (int i = 3; i < args.Length; i++)
            {
                string source;
                var dependencyName = args[i];
                using (var dependencyFile = MemoryMappedFile.OpenExisting(dependencyName, MemoryMappedFileRights.Read))
                using (var stream = dependencyFile.CreateViewStream(0, 0, MemoryMappedFileAccess.Read))
                using (var streamReader = new StreamReader(stream))
                {
                    source = await streamReader.ReadToEndAsync();
                }
                if (dependencyName.EndsWith("cs"))
                {
                    sources.Add(new CSharpSourceCode(source, dependencyName, false));
                }
                else if (dependencyName.EndsWith("dothtml"))
                {
                    sources.Add(new DothtmlSourceCode(source, dependencyName, false));
                }
                else
                {
                    throw new InvalidOperationException($"Dependency '{dependencyName}' is of unknown type.");
                }
            }

            // read validated code
            var validatedSource = await Console.In.ReadToEndAsync();

            // pick validation service and validation source code type
            IValidationService validationService;
            switch (unit)
            {
                case CSharpUnit _:
                    validationService = new CSharpValidationService();
                    sources.Add(new CSharpSourceCode(validatedSource, "UserCode.cs", true));
                    break;
                case DothtmlUnit _:
                    validationService = new DothtmlValidationService();
                    sources.Add(new DothtmlSourceCode(validatedSource, "UserCode.dothtml", true));
                    break;
                default:
                    throw new InvalidOperationException($"Validation unit of type '{unit.GetType()}' is not supported.");
            }

            // validate and write out diagnostics
            var diagnostics = await validationService.Validate(unit.GetConstraints(), sources);
            var lightDiagnostics = diagnostics.Select(d => new LightDiagnostic(d))
                .ToArray();
            var formatter = new BinaryFormatter();
            using(var outStream = Console.OpenStandardOutput())
            {
                formatter.Serialize(outStream, lightDiagnostics);
            }
        }
    }
}