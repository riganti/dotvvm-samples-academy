using System;
using System.Diagnostics;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    [DebuggerDisplay(@"Binding: \{{BindingType.Name,nq}: {Value,nq}\}")]
    internal class ValidationBinding : ValidationTreeNode, IAbstractBinding
    {
        private readonly DothtmlBindingNode node;

        public ValidationBinding(
            DothtmlBindingNode node,
            Type bindingType,
            ValidationDataContextStack dataContext)
            : base(node)
        {
            BindingType = bindingType;
            this.node = node;
            DataContext = dataContext;
        }

        public Type BindingType { get; }

        public string Value => node.Value;

        ValidationDataContextStack DataContext { get; }

        IDataContextStack IAbstractBinding.DataContextTypeStack => DataContext;

        ITypeDescriptor IAbstractBinding.ResultType => throw new NotImplementedException();
    }
}