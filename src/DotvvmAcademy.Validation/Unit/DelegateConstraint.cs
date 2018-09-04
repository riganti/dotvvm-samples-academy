using System;

namespace DotvvmAcademy.Validation.Unit
{
    public class DelegateConstraint : IConstraint
    {
        private readonly Action<ConstraintContext> action;
        private readonly bool overwrite;

        public DelegateConstraint(Action<ConstraintContext> action, bool overwrite)
        {
            this.action = action;
            this.overwrite = overwrite;
        }

        public bool CanOverwrite(IConstraint other)
        {
            return overwrite;
        }

        public void Validate(ConstraintContext context)
        {
            action(context);
        }
    }
}