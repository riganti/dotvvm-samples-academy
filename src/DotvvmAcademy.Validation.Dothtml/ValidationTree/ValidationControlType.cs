using System;
using System.Diagnostics;
using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.ControlTree;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    [DebuggerDisplay("ControlType: {Type.FullName,nq}")]
    public class ValidationControlType : IControlType
    {
        public ValidationControlType(
            ValidationTypeDescriptor type,
            string virtualPath = null,
            ValidationTypeDescriptor dataContextRequirement = null)
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