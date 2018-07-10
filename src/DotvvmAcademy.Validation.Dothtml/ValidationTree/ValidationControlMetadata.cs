using System.Collections.Generic;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.ControlTree;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    internal class ValidationControlMetadata : ControlResolverMetadataBase
    {
        private readonly ValidationControlType controlType;

        public ValidationControlMetadata(ValidationControlType controlType) : base(controlType)
        {
            this.controlType = controlType;
        }

        public override DataContextChangeAttribute[] DataContextChangeAttributes { get; }

        public override DataContextStackManipulationAttribute DataContextManipulationAttribute { get; }

        protected override void LoadProperties(Dictionary<string, IPropertyDescriptor> result)
        {
            throw new System.NotImplementedException();
        }

        protected override void LoadPropertyGroups(List<PropertyGroupMatcher> result)
        {
            throw new System.NotImplementedException();
        }
    }
}