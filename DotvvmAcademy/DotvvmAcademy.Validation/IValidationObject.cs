namespace DotvvmAcademy.Validation
{
    public interface IValidationObject<out TValidate> : IActivatableObject
        where TValidate : Validate
    {
        TValidate Validate { get; }

        void AddError(string message);
    }
}