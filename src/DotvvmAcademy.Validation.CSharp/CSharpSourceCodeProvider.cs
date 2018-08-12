using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp
{
    public class CSharpSourceCodeProvider
    {
        private readonly ImmutableDictionary<string, CSharpSourceCode> sources;

        public CSharpSourceCodeProvider(IEnumerable<CSharpSourceCode> sources)
        {
            this.sources = sources.ToImmutableDictionary(s => s.FileName);
        }

        public CSharpSourceCode GetSourceCode(SyntaxTree tree)
        {
            if (sources.TryGetValue(tree.FilePath, out var source))
            {
                return source;
            }

            return null;
        }
    }
}