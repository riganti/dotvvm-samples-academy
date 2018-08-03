using DotvvmAcademy.CourseFormat;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Dothtml.Unit;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DotvvmAcademy.Validation.Unit
{
    public static class UnitExtensions
    {
        public const string CorrectCodeKey = "CorrectCode";
        public const string DefaultCodeKey = "DefaultCode";

        public static string GetCorrectCodePath(this IUnit unit)
        {
            return unit.GetSourcePath(CorrectCodeKey);
        }

        public static string GetDefaultCodePath(this IUnit unit)
        {
            return unit.GetSourcePath(DefaultCodeKey);
        }

        public static string GetSourcePath(this IUnit unit, string key)
        {
            var storage = unit.Provider.GetRequiredService<SourcePathStorage>();
            return storage.Get(key);
        }

        public static string GetValidatedLanguage(this IUnit unit)
        {
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
            unit.SetSourcePath(CorrectCodeKey, sourcePath);
        }

        public static void SetDefaultCodePath(this IUnit unit, string sourcePath)
        {
            unit.SetSourcePath(DefaultCodeKey, sourcePath);
        }

        public static void SetSourcePath(this IUnit unit, string key, string sourcepath)
        {
            var storage = unit.Provider.GetRequiredService<SourcePathStorage>();
            storage.Add(key, sourcepath);
        }
    }
}