using DotVVM.Framework.Controls.Infrastructure;
using DotVVM.Framework.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace DotvvmAcademy.Web.Hosting
{
    public class EVRegularLifecycleStrategy : RegularLifecycleStrategy
    {
        public EVRegularLifecycleStrategy(IDotvvmRequestContext context) : base(context)
        {
        }

        protected override Task<DotvvmView> GetView()
        {
            var hack = Context.Services.GetRequiredService<EVViewBuilder>();
            return hack.BuildView(Context);
        }
    }
}