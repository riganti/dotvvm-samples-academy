using DotVVM.Framework.Hosting;
using DotVVM.Framework.Security;
using System;

namespace DotvvmAcademy.Validation.Dothtml
{
    internal class FakeViewModelProtector : IViewModelProtector
    {
        private const string ExceptionMessage = "ViewModel protection is not supported in Dothtml Validation.";

        public string Protect(string serializedData, IDotvvmRequestContext context)
            => throw new NotSupportedException(ExceptionMessage);

        public byte[] Protect(byte[] plaintextData, params string[] purposes)
            => throw new NotSupportedException(ExceptionMessage);

        public string Unprotect(string protectedData, IDotvvmRequestContext context)
            => throw new NotSupportedException(ExceptionMessage);

        public byte[] Unprotect(byte[] protectedData, params string[] purposes)
            => throw new NotSupportedException(ExceptionMessage);
    }
}