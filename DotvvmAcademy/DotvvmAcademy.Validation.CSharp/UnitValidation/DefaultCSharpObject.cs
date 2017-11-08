using DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions;

namespace DotvvmAcademy.Validation.CSharp.UnitValidation
{
    public class DefaultCSharpObject : ICSharpObject
    {
        public DefaultCSharpObject(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                throw new System.ArgumentException("message", nameof(fullName));
            }

            FullName = fullName;
        }

        public string FullName { get; }
    }
}