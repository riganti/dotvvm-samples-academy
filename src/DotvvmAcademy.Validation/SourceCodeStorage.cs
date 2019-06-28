using System.Collections.Generic;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation
{
    public class SourceCodeStorage
    {
        public SourceCodeStorage(IEnumerable<ISourceCode> sources)
        {
            Sources = sources.ToImmutableDictionary(s => s.FileName);
        }

        public SourceCodeStorage(IDictionary<string, ISourceCode> sources)
        {
            Sources = sources.ToImmutableDictionary();
        }

        public ImmutableDictionary<string, ISourceCode> Sources { get; }
    }
}