using DotVVM.Framework.Configuration;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ResourceManagement;
using DotVVM.Framework.Routing;
using DotvvmAcademy.Web.Pages;
using DotvvmAcademy.Web.Pages.Step;

namespace DotvvmAcademy.Web
{
    public class DotvvmStartup : IDotvvmStartup
    {
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {
            ConfigureRoutes(config, applicationPath);
            ConfigureControls(config, applicationPath);
            ConfigureResources(config, applicationPath);
        }

        private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
        {
            config.Markup.AddMarkupControl("cc", "LanguageSwitch", "Pages/LanguageSwitch.dotcontrol");
            config.Markup.AddCodeControls("cc", typeof(SvgToHtml));
            config.Markup.AddCodeControls("cc", typeof(MonacoEditor));
        }

        private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
        {
            config.Resources.Register(
                name: "MonacoLoader",
                resource: new ScriptResource(new FileResourceLocation("~/wwwroot/libs/monaco/loader.js")));
            config.Resources.Register(
                name: "jQuery",
                resource: new ScriptResource(new FileResourceLocation("~/wwwroot/libs/jquery/jquery.js")));
            config.Resources.Register(
                name: "AppJS",
                resource: new ScriptResource(new FileResourceLocation("~/wwwroot/scripts/app.js"))
                {
                    Dependencies = new[] { "jQuery", "MonacoLoader" }
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
        }
    }
}