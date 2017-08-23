namespace DotvvmAcademy.BL.Validation
{
    public abstract class ValidationObject<TValidate>
        where TValidate : Validate
    {
        internal ValidationObject(TValidate validate, bool isActive = true)
        {
            Validate = validate;
            IsActive = isActive;
        }

        public bool IsActive { get; }

        public TValidate Validate { get; }

        protected abstract void AddError(string message);

        protected void AddError(string message, int startPosition, int endPosition) => Validate.AddError(message, startPosition, endPosition);

        protected void AddGlobalError(string message) => Validate.AddGlobalError(message);
    }
}