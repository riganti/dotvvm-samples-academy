using DotvvmAcademy.CourseFormat;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Dothtml.Unit;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DotvvmAcademy.Validation.Unit
{
    public static class UnitExtensions
    {
        public static string GetValidatedLanguage(this IValidationUnit unit)
        {
            // TODO: Make language validation pluggable into CourseFormat
            switch (unit)
            {
                case CSharpUnit csharp:
                    return ValidatedLanguages.CSharp;

                case DothtmlUnit dothtml:
                    return ValidatedLanguages.Dothtml;

                default:
                    throw new NotSupportedException($"{nameof(IValidationUnit)} type '{unit.GetType()}' is not supported.");
            }
        }

        public static void SetCorrect(this IValidationUnit unit, string correctPath)
        {
            var options = unit.Provider.GetRequiredService<CodeTaskOptions>();
            options.CorrectCodePath = unit.GetAbsolutePath(correctPath);
        }

        public static CodeTaskOptions GetCodeTaskOptions(this IValidationUnit unit)
        {
            return unit.Provider.GetRequiredService<CodeTaskOptions>();
        }

        [Obsolete("Name too long")]
        public static void SetCorrectCodePath(this IValidationUnit unit, string sourcePath)
        {
            var configuration = unit.Provider.GetRequiredService<CodeTaskOptions>();
            // TODO: Minimize string operations
            var scriptDirectory = SourcePath.GetParent(configuration.ScriptPath);
            var absolutePath = SourcePath.Normalize(SourcePath.Combine(scriptDirectory, sourcePath));
            configuration.CorrectCodePath = absolutePath;
        }

        public static void SetDefault(this IValidationUnit unit, string defaultPath)
        {
            var options = unit.Provider.GetRequiredService<CodeTaskOptions>();
            options.DefaultCodePath = unit.GetAbsolutePath(defaultPath);
        }

        [Obsolete("Name too long")]
        public static void SetDefaultCodePath(this IValidationUnit unit, string sourcePath)
        {
            var configuration = unit.Provider.GetRequiredService<CodeTaskOptions>();
            // TODO: Minimize string operations
            var scriptDirectory = SourcePath.GetParent(configuration.ScriptPath);
            var absolutePath = SourcePath.Normalize(SourcePath.Combine(scriptDirectory, sourcePath));
            configuration.DefaultCodePath = absolutePath;
        }

        public static void SetFileName(this IValidationUnit unit, string fileName)
        {
            var configuration = unit.Provider.GetRequiredService<CodeTaskOptions>();
            configuration.FileName = fileName;
        }

        public static void SetSourcePath(this IValidationUnit unit, string fileName, string sourcePath)
        {
            var configuration = unit.Provider.GetRequiredService<CodeTaskOptions>();
            var absolutePath = SourcePath.Normalize(SourcePath.Combine(SourcePath.GetParent(configuration.ScriptPath), sourcePath));
            configuration.SourcePaths.AddOrUpdate(fileName, absolutePath, (k, v) => absolutePath);
        }

        private static string GetAbsolutePath(this IValidationUnit unit, string scriptRelativePath)
        {
            var options = unit.Provider.GetRequiredService<CodeTaskOptions>();
            var scriptDirectory = SourcePath.GetParent(options.ScriptPath);
            var absolutePath = SourcePath.Normalize(SourcePath.Combine(scriptDirectory, scriptRelativePath));
            return absolutePath;
        }
    }
}