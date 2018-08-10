using DotvvmAcademy.CourseFormat;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Dothtml.Unit;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DotvvmAcademy.Validation.Unit
{
    public static class UnitExtensions
    {
        public static void AddSourcePath(this IUnit unit, string sourcePath)
        {
            unit.Provider.GetRequiredService<SourcePathStorage>().Add(sourcePath);
        }

        public static void SetCorrectCodePath(this IUnit unit, string sourcePath)
        {
            var configuration = unit.Provider.GetRequiredService<CodeTaskConfiguration>();
            // TODO: Minimize string operations
            var scriptDirectory = SourcePath.GetParent(configuration.ScriptPath);
            var absolutePath = SourcePath.Normalize(SourcePath.Combine(scriptDirectory, sourcePath));
            configuration.CorrectCodePath = absolutePath;
        }

        public static void SetDefaultCodePath(this IUnit unit, string sourcePath)
        {
            var configuration = unit.Provider.GetRequiredService<CodeTaskConfiguration>();
            // TODO: Minimize string operations
            var scriptDirectory = SourcePath.GetParent(configuration.ScriptPath);
            var absolutePath = SourcePath.Normalize(SourcePath.Combine(scriptDirectory, sourcePath));
            configuration.DefaultCodePath = absolutePath;
        }

        public static string GetValidatedLanguage(this IUnit unit)
        {
            // TODO: Make language validation pluggable into CourseFormat
            switch (unit)
            {
                case CSharpUnit csharp:
                    return ValidatedLanguages.CSharp;

                case DothtmlUnit dothtml:
                    return ValidatedLanguages.Dothtml;

                default:
                    throw new NotSupportedException($"{nameof(IUnit)} type '{unit.GetType()}' is not supported.");
            }
        }
    }
}