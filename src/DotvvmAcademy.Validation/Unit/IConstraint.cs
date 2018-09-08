namespace DotvvmAcademy.Validation.Unit
{
    public interface IConstraint
    {
        void Validate(ConstraintContext context);

        bool CanOverwrite(IConstraint other);

        int GetOverwriteHashCode();
    }
}