using DotvvmAcademy.CourseFormat;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Dothtml.Unit;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DotvvmAcademy.Validation.Unit
{
    public static class UnitExtensions
    {
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

        public static void SetCorrectCodePath(this IUnit unit, string sourcePath)
        {
            var configuration = unit.Provider.GetRequiredService<CodeTaskOptions>();
            // TODO: Minimize string operations
            var scriptDirectory = SourcePath.GetParent(configuration.ScriptPath);
            var absolutePath = SourcePath.Normalize(SourcePath.Combine(scriptDirectory, sourcePath));
            configuration.CorrectCodePath = absolutePath;
        }

        public static void SetDefaultCodePath(this IUnit unit, string sourcePath)
        {
            var configuration = unit.Provider.GetRequiredService<CodeTaskOptions>();
            // TODO: Minimize string operations
            var scriptDirectory = SourcePath.GetParent(configuration.ScriptPath);
            var absolutePath = SourcePath.Normalize(SourcePath.Combine(scriptDirectory, sourcePath));
            configuration.DefaultCodePath = absolutePath;
        }

        public static void SetFileName(this IUnit unit, string fileName)
        {
            var configuration = unit.Provider.GetRequiredService<CodeTaskOptions>();
            configuration.FileName = fileName;
        }

        public static void SetSourcePath(this IUnit unit, string fileName, string sourcePath)
        {
            var configuration = unit.Provider.GetRequiredService<CodeTaskOptions>();
            var absolutePath = SourcePath.Normalize(SourcePath.Combine(SourcePath.GetParent(configuration.ScriptPath), sourcePath));
            configuration.SourcePaths.AddOrUpdate(fileName, absolutePath, (k, v) => absolutePath);
        }
    }
}