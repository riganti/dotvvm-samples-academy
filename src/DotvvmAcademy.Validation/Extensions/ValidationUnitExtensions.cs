using System;

namespace DotvvmAcademy.Validation.Unit
{
    public static class ValidationUnitExtensions
    {
        public static void AddDelegateConstraint(this IValidationUnit unit, Action<ConstraintContext> constraint)
        {
            unit.Constraints.Add(new DelegateConstraint(constraint));
        }
    }
}