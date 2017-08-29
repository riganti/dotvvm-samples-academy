namespace DotvvmAcademy.Validation
{
    public abstract class ValidationObject<TValidate> : ActivatableObject, IValidationObject<TValidate>
        where TValidate : Validate
    {
        internal ValidationObject(TValidate validate, bool isActive = true) : base(isActive)
        {
            Validate = validate;
        }

        public TValidate Validate { get; }

        public abstract void AddError(string message);

        protected void AddError(string message, int startPosition, int endPosition) => Validate.AddError(message, startPosition, endPosition, this);

        protected void AddGlobalError(string message) => Validate.AddGlobalError(message, this);
    }
}