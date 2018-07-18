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
        private readonly CourseWorkspace workspace;
        private readonly MarkdownExtractor extractor;

        public DefaultViewModel(CourseWorkspace workspace, MarkdownExtractor extractor)
        {
            this.workspace = workspace;
            this.extractor = extractor;
        }

        [Bind(Direction.ServerToClientFirstRequest)]
        public ImmutableArray<MarkdownLessonInfo> Lessons { get; set; }

        [Bind(Direction.ServerToClientFirstRequest)]
        public ImmutableArray<string> CurrentSteps { get; set; }

        public override async Task Load()
        {
            var variant = await workspace.LoadVariant($"/{Language}");
            var lessonTasks = variant.Lessons.Select(p => workspace.LoadLesson(p.Value));
            var lessons = await Task.WhenAll(lessonTasks);
            Lessons = lessons.Select(l => extractor.Extract(l)).ToImmutableArray();
            CurrentSteps = lessons
                .Select(l => l
                    .Steps.Values
                    .OrderBy(s => s.Moniker)
                    .Select(s=>s.Moniker)
                    .FirstOrDefault())
                .ToImmutableArray();
        }
    }
}