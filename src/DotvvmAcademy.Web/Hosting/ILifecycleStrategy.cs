using DotVVM.Framework.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotvvmAcademy.Web.Hosting
{
    public interface ILifecycleStrategy
    {
        bool CanProcess();

        Task PreInit();

        Task Init();

        Task Load();

        Task PreRender();

        Task PreRenderCompleted();

        Task Render();

        Task OnException(Exception exception);
    }
}
