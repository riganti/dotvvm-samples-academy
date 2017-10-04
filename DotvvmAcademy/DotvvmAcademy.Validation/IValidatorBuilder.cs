public namespace DotvvmAcademy.Validation {
    public interface IValidatorBuilder<TValidator> {
        TValidator Build();
    }
}