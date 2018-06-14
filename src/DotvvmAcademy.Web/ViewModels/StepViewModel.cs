using DotVVM.Framework.ViewModel;
using DotvvmAcademy.BL;
using DotvvmAcademy.CourseFormat;
using Markdig;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.Web.ViewModels
{
    public class StepViewModel : SiteViewModel
    {
        private readonly LessonFacade facade;
        private readonly ValidationService validation;
        private readonly CourseWorkspace workspace;
        private LessonDTO lesson;
        private IStep step;
        private ICodeTask task;

        public StepViewModel(LessonFacade facade, CourseWorkspace workspace, ValidationService validation)
        {
            this.facade = facade;
            this.workspace = workspace;
            this.validation = validation;
        }

        public string Code { get; set; }

        [Bind(Direction.None)]
        public string CodeLanguage { get; set; }

        [Bind(Direction.ServerToClient)]
        public List<ICodeTaskDiagnostic> Diagnostics { get; set; }

        [Bind(Direction.None)]
        public bool IsNextVisible { get; set; }

        [Bind(Direction.None)]
        public bool IsPreviousVisible { get; set; }

        [FromRoute("Lesson")]
        public string Lesson { get; set; }

        [Bind(Direction.ServerToClient)]
        public string NextStep { get; set; }

        [Bind(Direction.ServerToClient)]
        public string PreviousStep { get; set; }

        [FromRoute("Step"), Bind(Direction.None)]
        public string Step { get; set; }

        [Bind(Direction.None)]
        public string Text { get; set; }

        public override async Task Load()
        {
            lesson = await facade.GetLesson(Language, Lesson);
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
            step = await workspace.LoadStep($"/{Language}/{Lesson}/{Step}");
            Text = Markdown.ToHtml(step.Text);
            if (!Context.IsPostBack && step.CodeTaskId != null)
            {
                task = await workspace.LoadCodeTask(step.CodeTaskId);
                Code = task.Code;
                CodeLanguage = task.Id.Language == ".cs" ? "csharp" : "html";
            }
        }

        public async Task Validate()
        {
            if (workspace.CodeTaskIds.TryGetValue($"/{Language}/{Lesson}/{Step}", out var id))
            {
                Diagnostics = (await validation.Validate(id, Code)).ToList();
            }
        }
    }
}