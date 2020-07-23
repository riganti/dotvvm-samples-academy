using DotVVM.Framework.ViewModel;
using DotvvmAcademy.CourseFormat;
using DotvvmAcademy.Web.Resources.Localization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.Web.Pages.Default
{
    public class DefaultViewModel : SiteViewModel
    {
        private readonly CourseWorkspace workspace;
        private readonly IWebHostEnvironment environment;

        public DefaultViewModel(CourseWorkspace workspace, IWebHostEnvironment environment)
        {
            this.workspace = workspace;
            this.environment = environment;
        }

        [Bind(Direction.ServerToClientFirstRequest)]
        public LessonDetail FirstLesson { get; set; }

        [Bind(Direction.ServerToClientFirstRequest)]
        public List<LessonDetail> Lessons { get; set; } = new List<LessonDetail>();

        public override async Task Load()
        {
            Languages = DotvvmStartup.EnabledCultures
                .Where(l => l != LanguageMoniker)
                .Select(LanguageOption.Create)
                .ToList();

            var variants = workspace.CurrentCourse.Lessons
                .SelectMany(l => l.Variants)
                .Where(v => v.Moniker == LanguageMoniker);
            if (environment.IsProduction())
            {
                variants = variants.Where(v => v.Status == LessonStatus.Released);
            }
            foreach(var variant in variants)
            {
                var annotation = await workspace.GetLessonVariantAnnotation(variant);
                Lessons.Add(new LessonDetail
                {
                    FirstStep = variant.Steps.FirstOrDefault()?.Moniker,
                    AnnotationHtml = annotation,
                    ImageUrl = variant.ImageUrl,
                    Moniker = variant.LessonMoniker,
                    Name = variant.Name
                });
            }
            FirstLesson = Lessons.FirstOrDefault();
            await base.Load();
        }
    }
}
