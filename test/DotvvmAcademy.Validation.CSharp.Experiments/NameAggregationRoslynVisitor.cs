using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DotvvmAcademy.Validation.CSharp.Experiments
{
    public class NameAggregationRoslynVisitor : SymbolVisitor
    {
        private SymbolDisplayFormat format;

        public NameAggregationRoslynVisitor()
        {
            var arguments = new object[] {
                    3,
                    SymbolDisplayGlobalNamespaceStyle.Omitted,
                    SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
                    SymbolDisplayGenericsOptions.IncludeTypeParameters,
                    SymbolDisplayMemberOptions.IncludeContainingType | SymbolDisplayMemberOptions.IncludeType | SymbolDisplayMemberOptions.IncludeParameters,
                    SymbolDisplayDelegateStyle.NameOnly,
                    SymbolDisplayExtensionMethodStyle.StaticMethod,
                    SymbolDisplayParameterOptions.IncludeType,
                    SymbolDisplayPropertyStyle.NameOnly,
                    SymbolDisplayLocalOptions.None,
                    SymbolDisplayKindOptions.None,
                    SymbolDisplayMiscellaneousOptions.ExpandNullable
                };
            var formatType = typeof(SymbolDisplayFormat);
            var optionsType = formatType.Assembly.GetType("Microsoft.CodeAnalysis.SymbolDisplayCompilerInternalOptions");
            var constructor = formatType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance).Single();
            format = (SymbolDisplayFormat)constructor.Invoke(arguments);
        }

        public List<string> FullNames { get; set; } = new List<string>();

        public override void DefaultVisit(ISymbol symbol)
        {
            FullNames.Add(symbol.ToDisplayString(format));
        }

        public override void VisitAssembly(IAssemblySymbol symbol)
        {
            base.VisitAssembly(symbol);
            VisitNamespace(symbol.GlobalNamespace);
        }

        public override void VisitNamedType(INamedTypeSymbol symbol)
        {
            base.VisitNamedType(symbol);
            foreach (var member in symbol.GetMembers())
            {
                member.Accept(this);
            }
        }

        public override void VisitNamespace(INamespaceSymbol symbol)
        {
            base.VisitNamespace(symbol);
            foreach (var member in symbol.GetMembers())
            {
                member.Accept(this);
            }
        }
    }
}