using DotVVM.Framework.ViewModel;
using DotvvmAcademy.CourseFormat;
using DotvvmAcademy.Meta;
using DotvvmAcademy.Validation.Unit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.Web.Pages.Step
{
    public class StepViewModel : SiteViewModel
    {
        private readonly CourseWorkspace workspace;
        private CourseFormat.Step step;

        public StepViewModel(CourseWorkspace workspace)
        {
            this.workspace = workspace;
        }

        public CodeTaskDetail CodeTask { get; set; }

        public bool IsFinishDialogVisible { get; set; }

        [FromRoute("Lesson")]
        [Bind(Direction.ServerToClientFirstRequest)]
        public string LessonMoniker { get; set; }

        [FromRoute("Step")]
        [Bind(Direction.ServerToClientFirstRequest)]
        public string StepMoniker { get; set; }

        [Bind(Direction.ServerToClientFirstRequest)]
        public string LessonName { get; set; }

        [Bind(Direction.ServerToClientFirstRequest)]
        public StepDetail Step { get; set; }

        [Bind(Direction.ServerToClientFirstRequest)]
        public IEnumerable<StepListDetail> Steps { get; set; }

        public override async Task Load()
        {
            // get available languages
            var lesson = workspace.CurrentCourse.GetLesson(LessonMoniker);
            Languages = lesson.Variants.Where(v => v.Moniker != LanguageMoniker)
                .Select(v => LanguageOption.Create(v.Moniker))
                .ToList();

            // load step
            var variant = lesson.GetVariant(LanguageMoniker);
            LessonName = variant.Name;
            Steps = variant.Steps.Select(s => new StepListDetail
            {
                Moniker = s.Moniker,
                Name = s.Name
            });
            step = variant.GetStep(StepMoniker);
            var index = variant.Steps.IndexOf(step);
            var text = await workspace.GetStepText(step);
            Step = new StepDetail
            {
                Html = text,
                Name = step.Name,
                PreviousStep = variant.Steps.ElementAtOrDefault(index - 1)?.Moniker,
                NextStep = variant.Steps.ElementAtOrDefault(index + 1)?.Moniker,
                HasCodeTask = step.CodeTask != null,
                HasEmbeddedView = step.EmbeddedView != null,
                HasArchive = step.Archive != null
            };
            if (Step.HasCodeTask)
            {
                if (!Context.IsPostBack)
                {
                    CodeTask = new CodeTaskDetail();
                }
                CodeTask.SourcePath = step.CodeTask.Path;
                if (!Context.IsPostBack)
                {
                    await Reset();
                }
            }

            await base.Load();
        }

        public async Task Reset()
        {
            CodeTask = new CodeTaskDetail
            {
                Code = await workspace.GetFileContents(step.CodeTask.DefaultPath),
                CodeLanguage = step.CodeTask.CodeLanguage
            };
        }

        public async Task ShowSolution()
        {
            CodeTask.Code = await workspace.GetFileContents(step.CodeTask.CorrectPath);
        }

        public async Task Validate()
        {
            MonacoMarker GetMarker(CodeTaskDiagnostic diagnostic)
            {
                int startLineNumber, startColumn;
                int endLineNumber, endColumn;
                if (diagnostic.Start == -1 && diagnostic.End == -1)
                {
                    // a global diagnostic should remain global
                    (startLineNumber, startColumn) = (-1, -1);
                    (endLineNumber, endColumn) = (-1, -1);
                }
                else
                {
                    // interval [start, end) is half-open
                    (startLineNumber, startColumn) = MetaConvert.ToCoords(CodeTask.Code, diagnostic.Start);
                    (endLineNumber, endColumn) = MetaConvert.ToCoords(CodeTask.Code, diagnostic.End - 1);
                    // however, Monaco's intervals are half-open too, we have to compensate for that
                    endColumn++;
                }
                return new MonacoMarker(
                    message: diagnostic.Message,
                    severity: diagnostic.Severity.ToMonacoSeverity(),
                    startLineNumber: startLineNumber,
                    startColumn: startColumn,
                    endLineNumber: endLineNumber,
                    endColumn: endColumn);
            }

            try
            {
                var diagnostics = (await workspace.ValidateCodeTask(step.CodeTask, CodeTask.Code))
                    .ToArray();
                CodeTask.IsCodeCorrect = !diagnostics.Any(d => d.Severity == CodeTaskDiagnosticSeverity.Error);
                CodeTask.Markers = diagnostics
                    .Select(GetMarker)
                    .OrderBy(m => (m.StartLineNumber, m.StartColumn))
                    .OrderBy(m => m.Severity)
                    .ToList();
            }
            catch(CodeTaskException)
            {
                CodeTask.Markers.Clear();
                CodeTask.Markers.Add(new MonacoMarker("An error occured during validation.", MonacoSeverity.Error, -1, -1, -1, -1));
            }
        }
    }
}