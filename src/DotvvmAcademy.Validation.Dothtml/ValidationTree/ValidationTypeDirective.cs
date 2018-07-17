using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Binding.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System.Diagnostics;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    internal abstract class ValidationTypeDirective : ValidationDirective, IAbstractTypeSpecificationDirective
    {
        public ValidationTypeDirective(
            DothtmlDirectiveNode node,
            BindingParserNode typeSyntax,
            ValidationTypeDescriptor type)
            : base(node)
        {
            TypeSyntax = typeSyntax;
            Type = type;
        }

        public BindingParserNode TypeSyntax { get; }

        public ValidationTypeDescriptor Type { get; }

        BindingParserNode IAbstractTypeSpecificationDirective.NameSyntax => TypeSyntax;

        ITypeDescriptor IAbstractTypeSpecificationDirective.ResolvedType => Type;
    }
}