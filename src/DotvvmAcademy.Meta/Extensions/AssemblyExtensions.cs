using System.Collections.Generic;

namespace System.Reflection
{
    internal static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetTypes(this IEnumerable<Assembly> assemblies, string fullName)
        {
            foreach (var assembly in assemblies)
            {
                var type = assembly.GetType(fullName);
                if (type != default)
                {
                    yield return type;
                }
            }
        }
    }
}