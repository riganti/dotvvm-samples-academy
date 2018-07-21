using DotVVM.Framework.ViewModel;
using DotvvmAcademy.CourseFormat;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.Web.ViewModels
{
    public class DefaultViewModel : SiteViewModel
    {
        private readonly MarkdownExtractor extractor;
        private readonly CourseWorkspace workspace;

        public DefaultViewModel(CourseWorkspace workspace, MarkdownExtractor extractor)
        {
            this.workspace = workspace;
            this.extractor = extractor;
        }

        [Bind(Direction.ServerToClientFirstRequest)]
        public List<MarkdownLessonInfo> Lessons { get; set; }

        public override async Task Load()
        {
            var variant = await workspace.LoadVariant(Language);
            var lessonTasks = variant.Lessons.Select(l => workspace.LoadLesson(Language, l));
            var lessons = await Task.WhenAll(lessonTasks);
            Lessons = lessons.Select(l => extractor.Extract(l)).ToList();
        }
    }
}