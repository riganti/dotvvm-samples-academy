#nullable enable

using DotvvmAcademy.Meta;
using DotvvmAcademy.Validation;
using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Dothtml;
using DotvvmAcademy.Validation.Dothtml.Unit;
using DotvvmAcademy.Validation.Unit;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
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
    public static class Program
    {
        public const string DebuggerBreakOption = "--debugger-break";
        public const string HelpOption = "--help";
        public static List<LightDiagnostic> Diagnostics = new List<LightDiagnostic>();

        public static async Task<IValidationUnit?> GetValidationUnit(string scriptPath)
        {
            var scriptText = File.ReadAllText(scriptPath);
            var baseDir = new FileInfo(scriptPath).DirectoryName!;
            var scriptOptions = ScriptOptions.Default
                .AddReferences(
                    RoslynReference.FromName("netstandard"),
                    RoslynReference.FromName("System.Private.CoreLib"),
                    RoslynReference.FromName("System.Runtime"),
                    RoslynReference.FromName("System.Collections"),
                    RoslynReference.FromName("System.Reflection"),
                    RoslynReference.FromName("System.ComponentModel.Annotations"),
                    RoslynReference.FromName("System.ComponentModel.DataAnnotations"),
                    RoslynReference.FromName("System.Linq"),
                    RoslynReference.FromName("System.Linq.Expressions"), // Roslyn #23573
                    RoslynReference.FromName("Microsoft.CSharp"), // Roslyn #23573
                    RoslynReference.FromName("DotVVM.Framework"),
                    RoslynReference.FromName("DotVVM.Core"),
                    RoslynReference.FromName("DotvvmAcademy.Validation"),
                    RoslynReference.FromName("DotvvmAcademy.Validation.CSharp"),
                    RoslynReference.FromName("DotvvmAcademy.Validation.Dothtml"),
                    RoslynReference.FromName("DotvvmAcademy.Meta"))
                .WithFilePath(scriptPath)
            .WithSourceResolver(new SourceFileResolver(ImmutableArray.Create(baseDir), baseDir));
            var script = CSharpScript.Create(
                code: scriptText,
                options: scriptOptions);
            var compilation = script.GetCompilation();
            using var memoryStream = new MemoryStream();
            var emitResult = compilation.Emit(memoryStream);
            if (!emitResult.Success)
            {
                foreach (var diagnostic in emitResult.Diagnostics)
                {
                    Diagnostics.Add(new LightDiagnostic
                    {
                        Start = diagnostic.Location.SourceSpan.Start,
                        End = diagnostic.Location.SourceSpan.End,
                        Severity = diagnostic.Severity.ToValidationSeverity(),
                        Message = diagnostic.GetMessage(),
                        Source = Path.GetFileName(scriptPath),
                    });
                }
                return null;
            }
            var entryPoint = compilation.GetEntryPoint(default);
            if (entryPoint is null)
            {
                return null;
            }

            var entryTypeName = entryPoint.ContainingType.MetadataName;
            var entryTypeMethod = entryPoint.MetadataName;
            memoryStream.Position = 0;
            var scriptAssembly = AssemblyLoadContext.Default.LoadFromStream(memoryStream);
            var submissionType = scriptAssembly.GetType(entryTypeName)!;
            var factoryMethod = submissionType.GetMethod(entryTypeMethod)!;
            var unitProperty = submissionType.GetProperty("Unit")!;
            var submissionArray = new object[2]; // global object and Submission#0
            await (Task<object>)factoryMethod.Invoke(null, new object[] { submissionArray })!;
            return (IValidationUnit)unitProperty.GetValue(submissionArray[1])!;
        }

        public static async Task<ImmutableArray<ISourceCode>> GetDependencies(IEnumerable<string> paths)
        {
            var builder = ImmutableArray.CreateBuilder<ISourceCode>();
            foreach (var path in paths)
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

        public static void WriteDiagnostics()
        {
            using var outStream = Console.OpenStandardOutput();
            JsonSerializer.Serialize(new Utf8JsonWriter(outStream), Diagnostics);
        }

        private static string GetHelpText()
        {
            var executableName = Path.GetFileNameWithoutExtension(Environment.GetCommandLineArgs()[0]);
            return
$@"Usage: {executableName} [OPTION...] <SCRIPT_PATH> <CULTURE_ID> [DEPENDENCY...]

Options:
  {DebuggerBreakOption}  Wait for a debugger to attach.
  {HelpOption}            Show this help text.
";
        }

        public static async Task<int> Main(string[] args)
        {
            bool shouldBreak = false;
            int i = 0;
            bool isParsingOptions = true;
            while (isParsingOptions)
            {
                switch (args[i])
                {
                    case HelpOption:
                        Console.Write(GetHelpText());
                        return 0;
                    case DebuggerBreakOption:
                        shouldBreak = true;
                        break;
                    default:
                        isParsingOptions = false;
                        break;
                }
                i++;
            }
            --i;

            if (args.Length - i < 2)
            {
                Console.Error.Write(GetHelpText());
                return 1;
            }

            var scriptPath = args[i];
            var cultureId = args[++i];

            if (shouldBreak)
            {
                Debugger.Break();
            }

            var unit = await GetValidationUnit(scriptPath);
            if (unit is null)
            {
                WriteDiagnostics();
                return 1;
            }

            CultureInfo.CurrentCulture = new CultureInfo(cultureId);
            CultureInfo.CurrentUICulture = new CultureInfo(cultureId);
            var dependencies = await GetDependencies(args.Skip(i + 1));
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
            var validationDiagnostics = await validationService.Validate(unit.GetConstraints(), sources);
            Diagnostics.AddRange(validationDiagnostics.Select(d => new LightDiagnostic(d)));
            WriteDiagnostics();
            return 0;
        }
    }
}
