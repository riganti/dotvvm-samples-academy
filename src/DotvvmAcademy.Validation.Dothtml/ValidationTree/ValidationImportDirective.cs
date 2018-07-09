using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Binding.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    internal class ValidationImportDirective : ValidationDirective, IAbstractImportDirective
    {
        public ValidationImportDirective(
            DothtmlDirectiveNode node,
            BindingParserNode qualifiedNameSyntax,
            BindingParserNode aliasSyntax)
            : base(node)
        {
            QualifiedNameSyntax = qualifiedNameSyntax;
            AliasSyntax = aliasSyntax;
        }

        public BindingParserNode AliasSyntax { get; }

        public BindingParserNode QualifiedNameSyntax { get; }

        BindingParserNode IAbstractImportDirective.NameSyntax => QualifiedNameSyntax;

        bool IAbstractImportDirective.IsNamespace => throw new System.NotImplementedException();

        bool IAbstractImportDirective.IsType => throw new System.NotImplementedException();

        bool IAbstractImportDirective.HasAlias => AliasSyntax != null;

        bool IAbstractImportDirective.HasError => throw new System.NotImplementedException();
    }
}