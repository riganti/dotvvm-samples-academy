using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;
using DotvvmAcademy.Controls;
using System.Collections.Generic;
using System.Globalization;

namespace DotvvmAcademy
{
    public class DotvvmStartup : IDotvvmStartup
    {
        private List<CultureInfo> supportedCultures = new List<CultureInfo>
        {
            new CultureInfo("en"),
            new CultureInfo("cs"),
            new CultureInfo("ru")
        };

        public void Configure(DotvvmConfiguration config, string applicationPath)
        {
            ConfigureRoutes(config, applicationPath);
            ConfigureControls(config, applicationPath);
            ConfigureResources(config, applicationPath);
        }

        private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
        {
            config.Markup.AddCodeControls("cc", typeof(MonacoEditor));
        }

        private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
        {
            config.Resources.Register("font-awesome", new StylesheetResource()
            {
                Location = new UrlResourceLocation("https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.3.0/css/font-awesome.min.css"),
                RenderPosition = ResourceRenderPosition.Head
            });
            config.Resources.Register("montserrat", new StylesheetResource()
            {
                Location = new UrlResourceLocation("https://fonts.googleapis.com/css?family=Montserrat"),
                RenderPosition = ResourceRenderPosition.Head
            });
            config.Resources.Register("site", new StylesheetResource()
            {
                Location = new FileResourceLocation("~/wwwroot/Styles/site.css"),
                RenderPosition = ResourceRenderPosition.Head
            });
            config.Resources.Register("bootstrap", new ScriptResource()
            {
                Location = new FileResourceLocation("~/wwwroot/js/bootstrap.min.js"),
                RenderPosition = ResourceRenderPosition.Head
            });
            config.Resources.Register("jquery", new ScriptResource()
            {
                Location = new FileResourceLocation("~/wwwroot/js/jquery-2.2.4.js"),
                RenderPosition = ResourceRenderPosition.Head
            });
            config.Resources.Register("google-analytics", new ScriptResource()
            {
                Location = new FileResourceLocation("~/wwwroot/js/google-analytics.js"),
                RenderPosition = ResourceRenderPosition.Head
            });
            config.Resources.Register("monaco-loader", new ScriptResource()
            {
                Location = new FileResourceLocation("~/wwwroot/monaco-editor/min/vs/loader.js"),
                RenderPosition = ResourceRenderPosition.Body
            });
            config.Resources.Register("dotvvm-monaco", new ScriptResource()
            {
                Location = new FileResourceLocation("~/wwwroot/js/dotvvm-monaco.js"),
                RenderPosition = ResourceRenderPosition.Body,
                Dependencies = new[] { "monaco-loader" }
            });
        }

        private void ConfigureRoutes(DotvvmConfiguration config, string applicationPath)
        {
            config.RouteConstraints.Add("culture", new CultureRouteConstraint(supportedCultures));
            config.RouteTable.Add("Monaco", "monaco", "Views/MonacoTest.dothtml");
            config.RouteTable.Add("Default", "{Language?:culture}", "Views/Index.dothtml",
                new { Language = supportedCultures[0] });
            config.RouteTable.Add("ErrorNoCulture", "error/{StatusCode:int}", "Views/Error.dothtml",
                new { Language = supportedCultures[0], StatusCode = 404 });
            config.RouteTable.Add("Error", "{Language:culture}/error/{StatusCode:int}", "Views/Error.dothtml",
                new { Language = supportedCultures[0], StatusCode = 404 });
            config.RouteTable.Add("StepNoCulture", "{Moniker}/{StepIndex:int}", "Views/Step.dothtml",
                new { Language = supportedCultures[0], Moniker = "BasicMvvm", StepIndex = 0 });
            config.RouteTable.Add("Step", "{Language:culture}/{Moniker}/{StepIndex:int}", "Views/Step.dothtml",
                new { Language = supportedCultures[0], Moniker = "BasicMvvm", StepIndex = 0 });
        }
    }
}