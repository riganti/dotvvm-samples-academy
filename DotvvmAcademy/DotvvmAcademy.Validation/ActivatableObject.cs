namespace DotvvmAcademy.Validation
{
    public abstract class ActivatableObject : IActivatableObject
    {
        public ActivatableObject(bool isActive = true)
        {
            IsActive = isActive;
        }

        public bool IsActive { get; }
    }
}