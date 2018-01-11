using System;
using System.Globalization;
using System.Threading.Tasks;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ResourceManagement;
using Microsoft.Extensions.DependencyInjection;

namespace DotvvmAcademy
{
    public class LocalizedPresenter : IDotvvmPresenter
    {
        internal LocalizedPresenter()
        {
        }

        internal Func<IDotvvmRequestContext, CultureInfo> GetCulture { get; set; }

        internal Func<IDotvvmRequestContext, Task> NextPresenter { get; set; }

        public Task ProcessRequest(IDotvvmRequestContext context)
        {
            var culture = GetCulture(context);
            if (culture != null)
            {
                CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = culture;
            }

            return NextPresenter(context);
        }

        public static Func<LocalizedPresenter> BasedOnParameter(string name)
        {
            var presenter = new LocalizedPresenter();
            presenter.GetCulture = context =>
            {
                if (context.Parameters.TryGetValue(name, out var value) && value is CultureInfo culture)
                {
                    return culture;
                }

                return null;
            };

            presenter.NextPresenter = context =>
            {
                return context.Services.GetRequiredService<DotvvmPresenter>().ProcessRequest(context);
            };

            return () => presenter;
        }
    }
}