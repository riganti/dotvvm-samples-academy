using System;

namespace DotvvmAcademy.Validation.Unit
{
    public class DelegateConstraint : IConstraint
    {
        private readonly Action<ConstraintContext> action;

        public DelegateConstraint(Action<ConstraintContext> action)
        {
            this.action = action;
        }

        public bool CanOverwrite(IConstraint other)
        {
            return false;
        }

        public int GetOverwriteHashCode()
        {
            // It could be anything since no DelegateConstraint can be overwritten
            return action.GetHashCode();
        }

        public void Validate(ConstraintContext context)
        {
            action(context);
        }
    }
}