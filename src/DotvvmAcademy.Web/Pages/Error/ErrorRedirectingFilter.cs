using DotVVM.Framework.Hosting;
using DotVVM.Framework.Runtime.Filters;
using DotvvmAcademy.Web.Resources.Localization;
using Microsoft.ApplicationInsights;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.Web.Pages.Error
{
    public class ErrorRedirectingFilter : ExceptionFilterAttribute
    {
        private readonly TelemetryClient telemetryClient = new TelemetryClient();

        protected override Task OnCommandExceptionAsync(IDotvvmRequestContext context, ActionInfo actionInfo, Exception ex)
        {
            telemetryClient.TrackException(ex);
            Redirect(context);
            return Task.CompletedTask;
        }

        protected override Task OnPageExceptionAsync(IDotvvmRequestContext context, Exception ex)
        {
            telemetryClient.TrackException(ex);
            Redirect(context);
            return Task.CompletedTask;
        }

        protected override Task OnPresenterExceptionAsync(IDotvvmRequestContext context, Exception ex)
        {
            telemetryClient.TrackException(ex);
            Redirect(context);
            return Task.CompletedTask;
        }

        private void Redirect(IDotvvmRequestContext context)
        {
            var language = DotvvmStartup.DefaultCulture;
            if (context.Parameters.ContainsKey("Language")
                && DotvvmStartup.EnabledCultures.Contains(context.Parameters["Language"]))
            {
                language = (string)context.Parameters["Language"];
            }
            context.RedirectToRoute("Error", new { ErrorCode = 500, Language = language }, true);
        }
    }
}