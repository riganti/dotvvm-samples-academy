using DotVVM.Framework.Configuration;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Runtime;
using DotVVM.Framework.Security;
using DotVVM.Framework.ViewModel.Serialization;

namespace DotvvmAcademy.Web.Hosting
{
    public class EmbeddedViewPresenter : DotvvmPresenter
    {
        public EmbeddedViewPresenter(
            DotvvmConfiguration configuration,
            EmbeddedViewBuilder viewBuilder,
            EmbeddedViewModelLoader viewModelLoader,
            IViewModelSerializer viewModelSerializer,
            IOutputRenderer outputRender,
            ICsrfProtector csrfProtector,
            IViewModelParameterBinder viewModelParameterBinder,
            IStaticCommandServiceLoader staticCommandServiceLoader)
            : base(
                  configuration,
                  viewBuilder,
                  viewModelLoader,
                  viewModelSerializer,
                  outputRender,
                  csrfProtector,
                  viewModelParameterBinder,
                  staticCommandServiceLoader)
        {
        }
    }
}