using DotVVM.Framework.Binding;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class BindingMetadata
    {
        public DotvvmProperty Property { get; set; }

        public Type BindingType { get; set; }

        public ImmutableArray<string> AcceptedValues { get; set; }
    }
}
