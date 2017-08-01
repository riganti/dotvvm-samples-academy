using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;
using DotvvmAcademy.Controls;

namespace DotvvmAcademy
{
    public class DotvvmStartup : IDotvvmStartup
    {
        // For more information about this class, visit https://dotvvm.com/docs/tutorials/basics-project-structure
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {
            ConfigureRoutes(config, applicationPath);
            ConfigureControls(config, applicationPath);
            ConfigureResources(config, applicationPath);
        }

        private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
        {
            // register code-only controls and markup controls
            config.Markup.AddCodeControl("cc", typeof(AceEditor));

            config.Markup.AddMarkupControl("step", "InfoStep", "Controls/InfoStep.dotcontrol");
            config.Markup.AddMarkupControl("step", "ChoicesStep", "Controls/ChoicesStep.dotcontrol");
            config.Markup.AddMarkupControl("step", "DothtmlStep", "Controls/DothtmlStep.dotcontrol");
            config.Markup.AddMarkupControl("step", "CodeStep", "Controls/CodeStep.dotcontrol");
        }

        private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
        {
            // register custom resources and adjust paths to the built-in resources
            config.Resources.Register("ace", new ScriptResource()
            {
                Location = new LocalFileResourceLocation("~/wwwroot/Scripts/ace/ace.js")
            });
            config.Resources.Register("dotvvm-ace", new ScriptResource()
            {
                Location = new LocalFileResourceLocation("~/wwwroot/Scripts/dotvvm-ace.js"),
                Dependencies = new[] { "dotvvm", "ace" }
            });
        }

        private void ConfigureRoutes(DotvvmConfiguration config, string applicationPath)
        {
            config.RouteTable.Add("Default", "", "Views/default.dothtml");
            config.RouteTable.Add("Lesson", "lesson{Lesson}/step{Step}", "Views/lesson.dothtml");
            config.RouteTable.Add("Embedded", "elesson{Lesson}/step{Step}", "Views/embedded.dothtml");

            // Uncomment the following line to auto-register all dothtml files in the Views folder
            // config.RouteTable.AutoDiscoverRoutes(new DefaultRouteStrategy(config));
        }
    }
}