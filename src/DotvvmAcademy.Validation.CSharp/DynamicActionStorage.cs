using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp
{
    public class DynamicActionStorage
    {
        public List<Action<CSharpDynamicContext>> DynamicActions { get; } = new List<Action<CSharpDynamicContext>>();
    }
}