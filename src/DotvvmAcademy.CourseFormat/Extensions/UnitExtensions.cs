using DotvvmAcademy.CourseFormat;
using Microsoft.Extensions.DependencyInjection;

namespace DotvvmAcademy.Validation.Unit
{
    public static class UnitExtensions
    {
        public const string CorrectCodeKey = "CorrectCode";
        public const string DefaultCodeKey = "DefaultCode";

        public static void SetCorrectCodePath(this IUnit unit, string sourcePath)
            => unit.SetSourcePath(CorrectCodeKey, sourcePath);

        public static void SetDefaultCodePath(this IUnit unit, string sourcePath)
            => unit.SetSourcePath(DefaultCodeKey, sourcePath);

        internal static void SetSourcePath(this IUnit unit, string key, string sourcepath)
        {
            var storage = unit.Provider.GetRequiredService<SourcePathStorage>();
            storage.Add(key, sourcepath);
        }
    }
}