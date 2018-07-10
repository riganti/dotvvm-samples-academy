using System;
using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.ControlTree;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    internal class ValidationControlType : IControlType
    {
        public ValidationControlType(
            ValidationTypeDescriptor type,
            string virtualPath,
            ValidationTypeDescriptor dataContextRequirement)
        {
            Type = type;
            VirtualPath = virtualPath;
            DataContextRequirement = dataContextRequirement;
        }

        public ValidationTypeDescriptor Type { get; }

        public string VirtualPath { get; }

        public ValidationTypeDescriptor DataContextRequirement { get; }

        ITypeDescriptor IControlType.Type => Type;

        ITypeDescriptor IControlType.DataContextRequirement => DataContextRequirement;
    }
}