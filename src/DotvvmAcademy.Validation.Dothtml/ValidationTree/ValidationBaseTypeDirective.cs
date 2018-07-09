using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Binding.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    internal class ValidationBaseTypeDirective : ValidationTypeDirective, IAbstractBaseTypeDirective
    {
        public ValidationBaseTypeDirective(
            DothtmlDirectiveNode node,
            BindingParserNode typeSyntax,
            ValidationTypeDescriptor type)
            : base(node, typeSyntax, type)
        {
        }
    }
}