using DotVVM.Framework.Configuration;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ResourceManagement;
using DotvvmAcademy.Web.Hosting;
using DotvvmAcademy.Web.Pages.Error;
using DotvvmAcademy.Web.Pages.Step;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DotvvmAcademy.Web
{
    public class DotvvmStartup : IDotvvmStartup, IDotvvmServiceConfigurator
    {
        public const string EnglishCulture = "en";
        public const string CzechCulture = "cs";
        public const string RussianCulture = "ru";
        public const string DefaultCulture = EnglishCulture;

        public static string[] EnabledCultures = {
            EnglishCulture,
            CzechCulture,
            //RussianCulture // TODO: Uncomment when the translations are ready
        };

        public void Configure(DotvvmConfiguration config, string applicationPath)
        {
            // cause god knows what sort of culture is set on the machine
            config.DefaultCulture = EnglishCulture;

            // in debug I want to see the exception pages rather that the unspecific error pages
            if (!config.Debug)
            {
                config.Runtime.GlobalFilters.Add(new ErrorRedirectingFilter());
            }

            config.Markup.AddMarkupControl("cc",
                "LanguageSwitch", 
                "Pages/LanguageSwitch.dotcontrol");
            config.Markup.AddMarkupControl("cc",
                "DiagnosticList",
                "Pages/Step/DiagnosticList.dotcontrol");
            config.Markup.AddMarkupControl("cc",
                "FinishDialog",
                "Pages/Step/FinishDialog.dotcontrol");
            config.Markup.AddCodeControls("cc", typeof(MonacoEditor));

            if (config.Debug)
            {
                config.RouteTable.Add(
                    routeName: "Test",
                    url: "test/{LessonMoniker}/{VariantMoniker}/{StepMoniker}",
                    virtualPath: "Pages/Test/Test.dothtml",
                    defaultValues: new
                    {
                        LessonMoniker = "010_concepts",
                        VariantMoniker = "en",
                        StepMoniker = "10_viewmodel"
                    });
            }

            config.RouteTable.Add(
                routeName: "Default",
                url: "{Language}",
                virtualPath: "Pages/Default/default.dothtml",
                defaultValues: new { Language = "en" },
                presenterFactory: LocalizablePresenter.BasedOnParameter("Language"));
            config.RouteTable.Add(
                routeName: "Error",
                url: "{Language}/error/{ErrorCode}",
                virtualPath: "Pages/Error/Error.dothtml",
                defaultValues: new { Language = "en", ErrorCode = 500 },
                presenterFactory: LocalizablePresenter.BasedOnParameter("Language"));
            config.RouteTable.Add(
                routeName: "Step",
                url: "{Language}/{Lesson}/{Step}",
                virtualPath: "Pages/Step/step.dothtml",
                defaultValues: new { Language = "en" },
                presenterFactory: LocalizablePresenter.BasedOnParameter("Language"));
            config.RouteTable.Add(
                routeName: "EmbeddedView",
                url: "embeddedView/{Language}/{Lesson}/{Step}",
                presenterType: typeof(EmbeddedViewPresenter));
            config.RouteTable.Add(
                routeName: "Archive",
                url: "archive/{Language}/{Lesson}/{Step}",
                presenterType: typeof(ArchivePresenter));

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

        public void ConfigureServices(IDotvvmServiceCollection options)
        {
            options.AddDefaultTempStorages("temp");
        }
    }
}
