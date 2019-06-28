using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Binding.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System.Diagnostics;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    [DebuggerDisplay("ServiceInjectDirective: {Type.FullName,nq}")]
    public class ValidationServiceInjectDirective : ValidationDirective, IAbstractServiceInjectDirective
    {
        public ValidationServiceInjectDirective(
            DothtmlDirectiveNode node,
            SimpleNameBindingParserNode nameSyntax,
            BindingParserNode typeSyntax,
            ValidationTypeDescriptor type)
            : base(node)
        {
            NameSyntax = nameSyntax;
            TypeSyntax = typeSyntax;
            Type = type;
        }

        public SimpleNameBindingParserNode NameSyntax { get; }

        public BindingParserNode TypeSyntax { get; }

        public ValidationTypeDescriptor Type { get; }

        ITypeDescriptor IAbstractServiceInjectDirective.Type => Type;
    }
}