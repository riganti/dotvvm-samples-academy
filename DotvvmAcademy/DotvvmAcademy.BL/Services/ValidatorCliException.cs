using System;

namespace DotvvmAcademy.BL.Services
{
    public class ValidatorCliException : Exception
    {
        public ValidatorCliException(string message) : base(message)
        {
        }
    }
}