using DotVVM.Framework.Configuration;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ResourceManagement;
using DotvvmAcademy.Web.Hosting;
using DotvvmAcademy.Web.Pages;
using DotvvmAcademy.Web.Pages.Step;
using Microsoft.Extensions.DependencyInjection;

namespace DotvvmAcademy.Web
{
    public class DotvvmStartup : IDotvvmStartup, IDotvvmServiceConfigurator
    {
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {
            ConfigureRoutes(config, applicationPath);
            ConfigureControls(config, applicationPath);
            ConfigureResources(config, applicationPath);
        }

        public void ConfigureServices(IDotvvmServiceCollection options)
        {
            options.AddDefaultTempStorages("temp");
        }

        private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
        {
            config.Markup.AddMarkupControl("cc", "LanguageSwitch", "Pages/LanguageSwitch.dotcontrol");
            config.Markup.AddMarkupControl("cc", "DiagnosticList", "Pages/Step/DiagnosticList.dotcontrol");
            config.Markup.AddCodeControls("cc", typeof(SvgToHtml));
            config.Markup.AddCodeControls("cc", typeof(MonacoEditor));
            config.Markup.AddCodeControls("cc", typeof(LoadingPostBackHandler));
        }

        private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
        {
            config.Resources.Register(
                name: "MonacoLoader",
                resource: new ScriptResource(new FileResourceLocation("~/wwwroot/libs/monaco-editor/min/vs/loader.js")));
            config.Resources.Register(
                name: "AppJS",
                resource: new ScriptResource(new FileResourceLocation("~/wwwroot/scripts/app.js"))
                {
                    Dependencies = new[] { "MonacoLoader" }
                });
            config.Resources.Register(
                name: "StyleCSS",
                resource: new StylesheetResource(new FileResourceLocation("~/wwwroot/css/style.css")));
        }

        private void ConfigureRoutes(DotvvmConfiguration config, string applicationPath)
        {
            config.RouteTable.Add(
                routeName: "Step",
                url: "{Language}/{Lesson}/{Step}",
                virtualPath: "Pages/Step/step.dothtml",
                defaultValues: new { Language = "en" },
                presenterFactory: LocalizablePresenter.BasedOnParameter("Language"));
            config.RouteTable.Add(
                routeName: "Default",
                url: "{Language}",
                virtualPath: "Pages/Default/default.dothtml",
                defaultValues: new { Language = "en" },
                presenterFactory: LocalizablePresenter.BasedOnParameter("Language"));
            config.RouteTable.Add(
                routeName: "EmbeddedView",
                url: "embeddedView/{Language}/{Lesson}/{Step}",
                presenterType: typeof(EmbeddedViewPresenter));
            config.RouteTable.Add(
                routeName: "Archive",
                "archive/{Language}/{Lesson}/{Step}",
                typeof(ArchivePresenter));
        }
    }
}