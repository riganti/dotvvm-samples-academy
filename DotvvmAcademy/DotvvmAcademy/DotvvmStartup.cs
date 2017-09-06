using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;
using DotvvmAcademy.Controls;

namespace DotvvmAcademy
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
            config.Markup.AddCodeControls("cc", typeof(AceEditor));
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
        }

        private void ConfigureRoutes(DotvvmConfiguration config, string applicationPath)
        {
            config.RouteTable.Add("Default", "", "Views/LessonsOverview.dothtml");
            config.RouteTable.Add("LessonsOverview", "lessons/{Language}", "Views/LessonsOverview.dothtml",
                new { Language = "en"});
            config.RouteTable.Add("Step", "step/{Language}/{Moniker}/{StepIndex:int}", "Views/Step.dothtml",
                new { Language = "en", Moniker = "basicMvvm", StepIndex = 0 });
        }
    }
}