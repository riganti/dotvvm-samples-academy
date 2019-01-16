using System;

namespace DotvvmAcademy.Validation.CSharp.Constraints
{
    public class DynamicActionConstraint
    {
        public DynamicActionConstraint(Action<CSharpDynamicContext> action)
        {
            Action = action;
        }

        public Action<CSharpDynamicContext> Action { get; }

        public void Validate(DynamicActionStorage storage)
        {
            storage.DynamicActions.Add(Action);
        }
    }
}