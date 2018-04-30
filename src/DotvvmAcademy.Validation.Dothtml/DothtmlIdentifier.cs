using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.Validation.Dothtml
{
    public sealed class DothtmlIdentifier
    {
        public string ControlType { get; }

        public int Index { get; }

        public DothtmlIdentifier Parent { get; }
    }
}
