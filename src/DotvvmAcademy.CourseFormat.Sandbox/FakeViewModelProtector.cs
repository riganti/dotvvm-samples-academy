#nullable enable

using DotVVM.Framework.Hosting;

namespace DotVVM.Framework.Security
{
    // TODO: Remove this class if the same-named class in the Framework ever becomes public.
    internal class FakeViewModelProtector : IViewModelProtector
    {
        public string Protect(string serializedData, IDotvvmRequestContext context)
        {
            return "";
        }

        public byte[] Protect(byte[] plaintextData, params string[] purposes)
        {
            return new byte[0];
        }

        public string Unprotect(string protectedData, IDotvvmRequestContext context)
        {
            return "";
        }

        public byte[] Unprotect(byte[] protectedData, params string[] purposes)
        {
            return new byte[0];
        }
    }
}
