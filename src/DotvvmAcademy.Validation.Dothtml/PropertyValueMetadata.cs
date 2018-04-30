using DotVVM.Framework.Binding;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class PropertyValueMetadata
    {
        public DotvvmProperty Property{ get; set; }

        public ImmutableArray<object> AcceptedValues{ get; set; }
    }
}
