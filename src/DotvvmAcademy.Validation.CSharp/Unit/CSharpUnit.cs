using DotvvmAcademy.Validation.Unit;
using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public class CSharpUnit : ValidationUnit
    {
        public CSharpUnit(IServiceProvider provider) : base(provider)
        {
        }

        public ICollection<Action<CSharpDynamicContext>> DynamicActions { get; } = new List<Action<CSharpDynamicContext>>();
    }
}