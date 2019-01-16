using DotvvmAcademy.Meta;
using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp.Constraints
{
    internal class ConversionConstraint
    {
        public ConversionConstraint(NameNode source, NameNode destination)
        {
            Source = source;
            Destination = destination;
        }

        public NameNode Destination { get; }

        public NameNode Source { get; }

        public void Validate(CSharpValidationReporter reporter, MetaConverter converter, Compilation compilation)
        {
            var destination = converter.ToRoslyn(Destination)
                .OfType<ITypeSymbol>()
                .Single();
            var sources = converter.ToRoslyn(Source)
                .OfType<ITypeSymbol>();
            foreach (var source in sources)
            {
                if (!compilation.ClassifyCommonConversion(source, destination).Exists)
                {
                    converter.Report(
                        message: Resources.ERR_NoConversion,
                        arguments: new object[] { source, destination },
                        symbol: source);
                }
            }
        }
    }
}