using DotVVM.Framework.ViewModel;
using DotvvmAcademy.CourseFormat;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.Web.ViewModels
{
    public class DefaultViewModel : SiteViewModel
    {
        private readonly CourseWorkspace workspace;
        private readonly LessonRenderer lessonRenderer;

        public DefaultViewModel(CourseWorkspace workspace, LessonRenderer lessonRenderer)
        {
            this.workspace = workspace;
            this.lessonRenderer = lessonRenderer;
        }

        [Bind(Direction.ServerToClientFirstRequest)]
        public List<RenderedLesson> Lessons { get; set; }

        public override async Task Load()
        {
            var variant = await workspace.LoadVariant(Language);
            var lessonTasks = variant.Lessons.Select(l => workspace.LoadLesson(Language, l));
            var lessons = await Task.WhenAll(lessonTasks);
            Lessons = lessons.Select(l => lessonRenderer.Render(l)).ToList();
            await base.Load();
        }

        protected override async Task<IEnumerable<string>> GetAvailableLanguages()
        {
            var root = await workspace.LoadRoot();
            return root.Variants;
        }
    }
}