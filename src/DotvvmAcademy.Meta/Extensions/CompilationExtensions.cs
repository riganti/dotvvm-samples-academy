using System;
using System.Collections.Generic;

namespace Microsoft.CodeAnalysis
{
    internal static class CompilationExtensions
    {
        public static IEnumerable<INamespaceSymbol> GetGlobalNamespaces(this Compilation compilation)
        {
            yield return compilation.GlobalNamespace;

            foreach (var reference in compilation.References)
            {
                var symbol = compilation.GetAssemblyOrModuleSymbol(reference);
                switch (symbol)
                {
                    case IAssemblySymbol assembly:
                        yield return assembly.GlobalNamespace;
                        break;

                    case IModuleSymbol module:
                        yield return module.GlobalNamespace;
                        break;

                    default:
                        throw new NotSupportedException($"Compilation.GetAssemblyOrModuleSymbol returned '{symbol.GetType()}'. " +
                            $"That is unexpected.");
                }
            }
        }
    }
}