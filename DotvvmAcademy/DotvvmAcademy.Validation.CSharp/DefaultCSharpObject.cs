using DotvvmAcademy.Validation.CSharp.Abstractions;
using System;

namespace DotvvmAcademy.Validation.CSharp
{
    public class DefaultCSharpObject : ICSharpObject
    {
        private string fullName;

        public string FullName => fullName;

        public void SetUniqueFullName(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                this.fullName = fullName;
            }
            else
            {
                throw new InvalidOperationException($"The unique full name of this '{nameof(ICSharpObject)}' is already set.");
            }
        }
    }
}