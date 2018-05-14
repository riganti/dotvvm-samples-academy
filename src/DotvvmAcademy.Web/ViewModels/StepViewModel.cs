using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using DotvvmAcademy.BL;
using DotvvmAcademy.CourseFormat;
using Markdig;

namespace DotvvmAcademy.Web.ViewModels
{
    public class StepViewModel : SiteViewModel
    {
        private readonly CourseWorkspace workspace;
        private readonly LessonFacade facade;

        public StepViewModel(LessonFacade facade, CourseWorkspace workspace)
        {
            this.facade = facade;
            this.workspace = workspace;
        }

        public override async Task Load()
        {
            var lesson = await facade.GetLesson(Language, Lesson);
            var index = lesson.Steps.IndexOf(Step);
            IsPreviousVisible = index > 0;
            IsNextVisible = index < lesson.Steps.Count - 1;
            if (IsPreviousVisible)
            {
                PreviousStep = lesson.Steps[index - 1];
            }
            if (IsNextVisible)
            {
                NextStep = lesson.Steps[index + 1];
            }
            var step = await workspace.LoadStep($"/{Language}/{Lesson}/{Step}");
            Text = Markdown.ToHtml(step.Text);
        }

        [Bind(Direction.None)]
        public string Text { get; set; }

        [Bind(Direction.None)]
        public bool IsNextVisible { get; set; }

        [Bind(Direction.None)]
        public bool IsPreviousVisible { get; set; }

        [FromRoute("Lesson")]
        public string Lesson { get; set; }

        public string NextStep { get; set; }

        public string PreviousStep { get; set; }

        [FromRoute("Step"), Bind(Direction.None)]
        public string Step { get; set; }
    }
}