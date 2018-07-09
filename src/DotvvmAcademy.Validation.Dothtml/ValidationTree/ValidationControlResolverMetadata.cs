using System.Collections.Generic;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Compilation.ControlTree;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    internal class ValidationControlResolverMetadata : ControlResolverMetadataBase
    {
        public override DataContextChangeAttribute[] DataContextChangeAttributes => throw new System.NotImplementedException();

        public override DataContextStackManipulationAttribute DataContextManipulationAttribute => throw new System.NotImplementedException();

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