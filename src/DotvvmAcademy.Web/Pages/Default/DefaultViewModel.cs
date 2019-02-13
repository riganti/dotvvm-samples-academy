using DotVVM.Framework.ViewModel;
using DotvvmAcademy.CourseFormat;
using DotvvmAcademy.Web.Resources.Localization;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.Web.Pages.Default
{
    public class DefaultViewModel : SiteViewModel
    {
        private readonly CourseWorkspace workspace;
        private readonly IHostingEnvironment environment;

        public DefaultViewModel(CourseWorkspace workspace, IHostingEnvironment environment)
        {
            this.workspace = workspace;
            this.environment = environment;
        }

        [Bind(Direction.ServerToClientFirstRequest)]
        public LessonDetail FirstLesson { get; set; }

        [Bind(Direction.ServerToClientFirstRequest)]
        public List<LessonDetail> Lessons { get; set; }

        public override async Task Load()
        {
            Languages = UILanguage.AvailableLanguages
                .Where(l => l != LanguageMoniker)
                .Select(LanguageOption.Create)
                .ToList();

            var course = await workspace.LoadCourse();
            var lessonTasks = course.Lessons.Select(l => workspace.LoadLesson(l));
            var lessons = await Task.WhenAll(lessonTasks);
            var variantTasks = lessons.Where(l => l.Variants.Contains(LanguageMoniker))
                .Select(l => workspace.LoadLessonVariant(l.Moniker, LanguageMoniker));
            var variants = await Task.WhenAll(variantTasks);
            if (environment.IsProduction())
            {
                variants = variants.Where(v => v.Status == LessonStatus.Released)
                    .ToArray();
            }
            Lessons = variants.Select(v =>
            {
                return new LessonDetail
                {
                    FirstStep = v.Steps.FirstOrDefault(),
                    AnnotationHtml = v.Annotation,
                    ImageUrl = v.ImageUrl,
                    Moniker = v.LessonMoniker,
                    Name = v.Name
                };
            })
            .ToList();
            FirstLesson = Lessons.FirstOrDefault();
            await base.Load();
        }
    }
}