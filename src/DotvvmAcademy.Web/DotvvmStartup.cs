using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;
using DotVVM.Framework.Routing;
using DotVVM.Framework.Hosting;
using DotvvmAcademy.Web.Controls;

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

        private void ConfigureRoutes(DotvvmConfiguration config, string applicationPath)
        {
            config.RouteTable.Add(
                routeName: "Lesson",
                url: "{Lang}/lesson{Lesson}/step{Step}", 
                virtualPath: "Views/lesson.dothtml", 
                defaultValues: new { Lang = "en" },
                presenterFactory: LocalizablePresenter.BasedOnParameter("Lang"));
            config.RouteTable.Add(
                routeName:"Default", 
                url: "{Lang:alpha}", 
                virtualPath: "Views/default.dothtml", 
                defaultValues: new { Lang = "en" }, 
                presenterFactory: LocalizablePresenter.BasedOnParameter("Lang"));
            config.RouteTable.AutoDiscoverRoutes(new DefaultRouteStrategy(config));
        }

        private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
        {
            config.Markup.AddMarkupControl("cc", "LangSwitcher", "Controls/LangSwitcher.dotcontrol");
            config.Markup.AddCodeControls("cc", typeof(SvgToHtml));
        }

        private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
        {
            config.Resources.Register("jQuery", new ScriptResource(new FileResourceLocation("~/wwwroot/Scripts/jquery-2.2.4.min.js")));
            config.Resources.Register("AppJS", new ScriptResource(new FileResourceLocation("~/wwwroot/Scripts/app.min.js"))
            {
                Dependencies = new[] { "jQuery" }
            });

            config.Resources.Register("StyleCSS", new StylesheetResource(new FileResourceLocation("~/wwwroot/css/style.css")));
        }
    }
}
