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

        public void Validate(CSharpValidationReporter reporter, MetaConverter converter)
        {
            var symbols = converter.ToRoslyn(Node)
                .OfType<TSymbol>();
            foreach (var symbol in symbols)
            {
                if (!AllowedAccess.HasFlag(GetAllowedAccess(symbol.DeclaredAccessibility)))
                {
                    reporter.Report(
                        message: Resources.ERR_WrongAccessibility,
                        arguments: new object[] { symbol, AllowedAccess },
                        symbol: symbol);
                }
            }
        }

        private AllowedAccess GetAllowedAccess(Accessibility accessibility)
        {
            switch (accessibility)
            {
                case Accessibility.Private:
                    return AllowedAccess.Private;

                case Accessibility.ProtectedAndInternal:
                    return AllowedAccess.ProtectedAndInternal;

                case Accessibility.Protected:
                    return AllowedAccess.Protected;

                case Accessibility.Internal:
                    return AllowedAccess.Internal;

                case Accessibility.ProtectedOrInternal:
                    return AllowedAccess.ProtectedOrInternal;

                case Accessibility.Public:
                    return AllowedAccess.Public;

                default:
                    return AllowedAccess.None;
            }
        }
    }
}