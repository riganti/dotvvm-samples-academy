using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp
{
    public class CSharpSourceCodeProvider
    {
        private readonly ImmutableDictionary<Guid, CSharpSourceCode> dictionary;

        public CSharpSourceCodeProvider(IEnumerable<CSharpSourceCode> sources)
        {
            dictionary = sources.ToImmutableDictionary(s => s.Id);
        }

        public CSharpSourceCode GetSourceCode(SyntaxTree tree)
        {
            // TODO: Find a better way to identify SyntaxTrees
            if (Guid.TryParse(tree.FilePath, out var id) && dictionary.TryGetValue(id, out var source))
            {
                return source;
            }

            return null;
        }
    }
}