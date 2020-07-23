using DotvvmAcademy.Validation;
using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Dothtml;
using DotvvmAcademy.Validation.Dothtml.Unit;
using DotvvmAcademy.Validation.Unit;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.Loader;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat.Sandbox
{
    public class Program
    {
        public static async Task<IValidationUnit> GetValidationUnit(
            string mapPath,
            string entryTypeName,
            string entryTypeMethod)
        {
            using var scriptMap = MemoryMappedFile.CreateFromFile(
                path: mapPath,
                mode: FileMode.Open);
            using var mapStream = scriptMap.CreateViewStream(0, 0, MemoryMappedFileAccess.Read);
            var scriptAssembly = AssemblyLoadContext.Default.LoadFromStream(mapStream);
            var submissionType = scriptAssembly.GetType(entryTypeName);
            var factoryMethod = submissionType.GetMethod(entryTypeMethod);
            var unitProperty = submissionType.GetProperty("Unit");
            var submissionArray = new object[2]; // global object and Submission#0
            await (Task<object>)factoryMethod.Invoke(null, new object[] { submissionArray });
            return (IValidationUnit)unitProperty.GetValue(submissionArray[1]);
        }

        public static async Task<ImmutableArray<ISourceCode>> GetDependencies(IEnumerable<string> paths)
        {
            var builder = ImmutableArray.CreateBuilder<ISourceCode>();
            foreach(var path in paths)
            {
                var text = await File.ReadAllTextAsync(path);
                var extension = Path.GetExtension(path);
                ISourceCode source = Path.GetExtension(path) switch
                {
                    ".cs" => new CSharpSourceCode(text, Path.GetFileName(path), false),
                    ".dothtml" => new DothtmlSourceCode(text, Path.GetFileName(path), false),
                    _ => throw new InvalidOperationException($"Dependencies with the '{extension}' are unsupported.")
                };
                builder.Add(source);
            }
            return builder.ToImmutable();
        }

        public static async Task WriteDiagnostics(IEnumerable<IValidationDiagnostic> diagnostics)
        {
            using var outStream = Console.OpenStandardOutput();
            var lightDiagnostics = diagnostics.Select(d => new LightDiagnostic(d)).ToArray();
            await JsonSerializer.SerializeAsync(outStream, lightDiagnostics);
        }

        public static async Task Main(string[] args)
        {
            if (args.Length < 3)
            {
                throw new InvalidOperationException("The sandbox needs to be run with at least 4 arguments: " +
                    "name of the memory mapped file with the script assembly, " +
                    "name of the entry type inside the script assembly, " +
                    "name of the entry method inside the entry type, " +
                    "id of the current language.");
            }

            Debugger.Break();
            
            // load all the stuff
            var unit = await GetValidationUnit(args[0], args[1], args[2]);
            CultureInfo.CurrentCulture = new CultureInfo(args[3]);
            CultureInfo.CurrentUICulture = new CultureInfo(args[3]);
            var dependencies = await GetDependencies(args.Skip(4));
            var validatedText = await Console.In.ReadToEndAsync();
            ISourceCode validatedSource = unit switch
            {
                CSharpUnit _ => new CSharpSourceCode(validatedText, "UserCode.cs", true),
                DothtmlUnit _ => new DothtmlSourceCode(validatedText, "UserCode.dothtml", true),
                _ => throw new NotImplementedException()
            };
            var sources = dependencies.Add(validatedSource);

            // validate
            var validationService = new ValidationService();
            var diagnostics = await validationService.Validate(unit.GetConstraints(), sources);
            await WriteDiagnostics(diagnostics);
        }
    }
}
