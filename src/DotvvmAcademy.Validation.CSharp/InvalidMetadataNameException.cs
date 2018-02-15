using System;

namespace DotvvmAcademy.Validation.CSharp
{
    public class InvalidMetadataNameException : Exception
    {
        public InvalidMetadataNameException(string message) : base(message)
        {
        }
    }
}