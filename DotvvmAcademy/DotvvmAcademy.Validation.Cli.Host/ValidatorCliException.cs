using System;

namespace DotvvmAcademy.Validation.Cli.Host
{
    public class ValidatorCliException : Exception
    {
        public ValidatorCliException(string message) : base(message)
        {
        }
    }
}