using System;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    internal class ValidationBinding : ValidationTreeNode, IAbstractBinding
    {
        public ValidationBinding(
            DothtmlNode node,
            string code,
            Type bindingType,
            ValidationDataContextStack dataContext)
            : base(node)
        {
            BindingType = bindingType;
            Value = code;
            DataContext = dataContext;
        }

        public Type BindingType { get; }

        public string Value { get; }

        ValidationDataContextStack DataContext { get; }

        IDataContextStack IAbstractBinding.DataContextTypeStack => DataContext;

        ITypeDescriptor IAbstractBinding.ResultType => throw new NotImplementedException();
    }
}