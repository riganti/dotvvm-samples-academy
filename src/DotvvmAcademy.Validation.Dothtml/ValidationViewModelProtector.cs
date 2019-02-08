using DotVVM.Framework.Hosting;
using DotVVM.Framework.Security;

namespace DotvvmAcademy.Validation.Dothtml
{
    internal class ValidationViewModelProtector : IViewModelProtector
    {
        public string Protect(string serializedData, IDotvvmRequestContext context)
        {
            return serializedData;
        }

        public byte[] Protect(byte[] plaintextData, params string[] purposes)
        {
            return plaintextData;
        }

        public string Unprotect(string protectedData, IDotvvmRequestContext context)
        {
            return protectedData;
        }

        public byte[] Unprotect(byte[] protectedData, params string[] purposes)
        {
            return protectedData;
        }
    }
}