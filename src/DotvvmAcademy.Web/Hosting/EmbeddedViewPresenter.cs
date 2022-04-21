using DotVVM.Framework.Configuration;
using DotVVM.Framework.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using System;
using System.Threading.Tasks;

namespace DotvvmAcademy.Web.Hosting
{
    public class EmbeddedViewPresenter : IDotvvmPresenter
    {
        private readonly IDotvvmPresenter inner;

        public EmbeddedViewPresenter(
            IServiceProvider services,
            DotvvmConfiguration configuration,
            EmbeddedViewBuilder viewBuilder,
            EmbeddedViewModelLoader viewModelLoader)
        {
            inner = ActivatorUtilities.CreateInstance<DotvvmPresenter>(
                services,
                configuration,
                viewBuilder,
                viewModelLoader);
        }

        public async Task ProcessRequest(IDotvvmRequestContext context)
        {
            await inner.ProcessRequest(context);
        }
    }
}
