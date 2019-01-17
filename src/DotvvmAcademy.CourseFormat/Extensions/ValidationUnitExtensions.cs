using System.Collections.Generic;

namespace DotvvmAcademy.Validation.Unit
{
    public static class UnitExtensions
    {
        private const string CorrectKey = "CourseFormat_Correct";
        private const string DefaultKey = "CourseFormat_Default";
        private const string DependenciesKey = "CourseFormat_Dependencies";

        public static void AddDependency(this IValidationUnit unit, string sourcePath)
        {
            var dependencies = (List<string>)unit.GetAdditionalData(DependenciesKey);
            if (dependencies == null)
            {
                dependencies = new List<string>();
                unit.SetAdditionalData(DependenciesKey, dependencies);
            }
            dependencies.Add(sourcePath);
        }

        public static string GetCorrect(this IValidationUnit unit)
        {
            return (string)unit.GetAdditionalData(CorrectKey);
        }

        public static string GetDefault(this IValidationUnit unit)
        {
            return (string)unit.GetAdditionalData(DefaultKey);
        }

        public static IEnumerable<string> GetDependencies(this IValidationUnit unit)
        {
            return (IEnumerable<string>)unit.GetAdditionalData(DependenciesKey);
        }

        public static string GetValidatedLanguage(this IValidationUnit unit)
        {
            var name = unit.GetType().Name;
            if (name.EndsWith("Unit"))
            {
                name = name.Substring(0, name.Length - "Unit".Length);
            }
            return name.ToLower();
        }

        public static void RemoveDependency(this IValidationUnit unit, string sourcePath)
        {
            var dependencies = (List<string>)unit.GetAdditionalData(DependenciesKey);
            dependencies.Remove(sourcePath);
        }

        public static void SetCorrect(this IValidationUnit unit, string correctPath)
        {
            unit.SetAdditionalData(CorrectKey, correctPath);
        }

        public static void SetDefault(this IValidationUnit unit, string defaultPath)
        {
            unit.SetAdditionalData(DefaultKey, defaultPath);
        }
    }
}