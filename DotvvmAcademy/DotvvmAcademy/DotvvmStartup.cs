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
            config.Markup.AddCodeControls("cc", typeof(AceEditor));
            config.Markup.AddCodeControls("cc", typeof(MonacoEditor));
        }

        private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
        {
            config.Resources.Register("ace", new ScriptResource()
            {
                Location = new FileResourceLocation("~/wwwroot/Scripts/ace/ace.js")
            });
            config.Resources.Register("dotvvm-ace", new ScriptResource()
            {
                Location = new FileResourceLocation("~/wwwroot/Scripts/dotvvm-ace.js"),
                Dependencies = new[] { "dotvvm", "ace" }
            });
            config.Resources.Register("google-analytics", new ScriptResource()
            {
                Location = new FileResourceLocation("~/wwwroot/Scripts/google-analytics.js"),
                RenderPosition = ResourceRenderPosition.Head
            });
            config.Resources.Register("monaco-loader", new ScriptResource()
            {
                Location = new FileResourceLocation("~/wwwroot/monaco-editor/min/vs/loader.js"),
                RenderPosition = ResourceRenderPosition.Body
            });
        }

        private void ConfigureRoutes(DotvvmConfiguration config, string applicationPath)
        {
            config.RouteTable.Add("Monaco", "monaco", "Views/MonacoTest.dothtml");
            config.RouteConstraints.Add("culture", new CultureRouteConstraint(supportedCultures));
            config.RouteTable.Add("Default", "{Language?:culture}", "Views/LessonsOverview.dothtml",
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