using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Binding.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System.Diagnostics;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    [DebuggerDisplay("BaseTypeDirective: {Type.FullName,nq}")]
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