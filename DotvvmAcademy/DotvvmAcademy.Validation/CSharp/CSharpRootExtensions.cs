using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp
{
    public static class CSharpRootExtensions
    {
        public static void Usings(this CSharpRoot root, params string[] expectedUsings) => root.Usings((IEnumerable<string>)expectedUsings);
    }
}
