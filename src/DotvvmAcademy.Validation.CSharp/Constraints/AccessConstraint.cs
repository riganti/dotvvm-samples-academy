using DotvvmAcademy.Meta;
using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp.Constraints
{
    internal class AccessConstraint<TSymbol>
        where TSymbol : ISymbol
    {
        public AccessConstraint(NameNode node, AllowedAccess allowedAccess)
        {
            Node = node;
            AllowedAccess = allowedAccess;
        }

        public AllowedAccess AllowedAccess { get; }

        public NameNode Node { get; }

        public void Validate(IValidationReporter reporter, MetaConverter converter)
        {
            var symbols = converter.ToRoslyn(Node)
                .OfType<TSymbol>();
            foreach (var symbol in symbols)
            {
                if (!AllowedAccess.HasFlag(symbol.DeclaredAccessibility.ToAllowedAccess()))
                {
                    reporter.Report(
                        message: Resources.ERR_WrongAccessibility,
                        arguments: new object[] { symbol, AllowedAccess },
                        symbol: symbol);
                }
            }
        }
    }
}