using DotVVM.Framework.Compilation;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;

namespace DotvvmAcademy
{
    public class DotvvmStartup : IDotvvmStartup
    {
        // For more information about this class, visit https://dotvvm.com/docs/tutorials/basics-project-structure
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {
            config.Markup.ImportedNamespaces.Add(new NamespaceImport("DotvvmAcademy.Lessons"));

            ConfigureRoutes(config, applicationPath);
            ConfigureControls(config, applicationPath);
            ConfigureResources(config, applicationPath);
        }

        private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
        {
            // register code-only controls and markup controls
            config.Markup.AddCodeControls("cc", typeof(Controls.AceEditor));

            config.Markup.AddMarkupControl("step", "InfoStep", "Controls/InfoStep.dotcontrol");
            config.Markup.AddMarkupControl("step", "ChoicesStep", "Controls/ChoicesStep.dotcontrol");
            config.Markup.AddMarkupControl("step", "DothtmlStep", "Controls/DothtmlStep.dotcontrol");
            config.Markup.AddMarkupControl("step", "CodeStep", "Controls/CodeStep.dotcontrol");
        }

        private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
        {
            // register custom resources and adjust paths to the built-in resources
            config.Resources.Register("ace", new ScriptResource(
                new FileResourceLocation("~/wwwroot/Scripts/ace/ace.js")));
            config.Resources.Register("dotvvm-ace", new ScriptResource(
                new FileResourceLocation("~/wwwroot/Scripts/dotvvm-ace.js"))
            {
                Dependencies = new[] { "dotvvm", "ace" }
            });
        }

        private void ConfigureRoutes(DotvvmConfiguration config, string applicationPath)
        {
            config.RouteTable.Add("Lesson", "{Lang}/lesson{Lesson}/step{Step}", "Views/lesson.dothtml", presenterFactory: LocalizablePresenter.BasedOnParameter("Lang"));

            config.RouteTable.Add("Default", "{Lang:alpha}", "Views/default.dothtml", new { Lang = "en" }, presenterFactory: LocalizablePresenter.BasedOnParameter("Lang"));

            // Uncomment the following line to auto-register all dothtml files in the Views folder
            // config.RouteTable.AutoDiscoverRoutes(new DefaultRouteStrategy(config));
        }
    }
}