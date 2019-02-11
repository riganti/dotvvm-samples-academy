using DotVVM.Framework.ViewModel;
using DotvvmAcademy.CourseFormat;
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
            var variant = await workspace.LoadVariant(LanguageMoniker);
            var lessonTasks = variant.Lessons.Select(l => workspace.LoadLesson(LanguageMoniker, l));
            var lessons = await Task.WhenAll(lessonTasks);
            if (environment.IsProduction())
            {
                lessons = lessons.Where(l => l.Status == LessonStatus.Released)
                    .ToArray();
            }
            Lessons = lessons.Select(l =>
            {
                return new LessonDetail
                {
                    FirstStep = l.Steps.FirstOrDefault(),
                    Html = l.Annotation,
                    ImageUrl = l.ImageUrl,
                    Moniker = l.Moniker,
                    Name = l.Name
                };
            })
            .ToList();
            FirstLesson = Lessons.FirstOrDefault();
            await base.Load();
        }

        protected override async Task<IEnumerable<string>> GetAvailableLanguageMonikers()
        {
            var root = await workspace.LoadRoot();
            return root.Variants;
        }
    }
}