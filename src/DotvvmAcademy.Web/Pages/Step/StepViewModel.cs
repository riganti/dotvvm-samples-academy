using DotVVM.Framework.ViewModel;
using DotvvmAcademy.CourseFormat;
using DotvvmAcademy.Meta;
using DotvvmAcademy.Validation.Unit;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.Web.Pages.Step
{
    public class StepViewModel : SiteViewModel
    {
        private readonly ICourseEnvironment environment;
        private readonly CodeTaskValidator validator;
        private readonly CourseWorkspace workspace;
        public bool IsFinishDialogVisible { get; set; }

        public StepViewModel(
            CourseWorkspace workspace,
            ICourseEnvironment environment,
            CodeTaskValidator validator)
        {
            this.workspace = workspace;
            this.environment = environment;
            this.validator = validator;
        }

        public CodeTaskDetail CodeTask { get; set; }

        [FromRoute("Lesson")]
        [Bind(Direction.ServerToClientFirstRequest)]
        public string LessonMoniker { get; set; }

        [Bind(Direction.ServerToClientFirstRequest)]
        public string LessonName { get; set; }

        [Bind(Direction.ServerToClientFirstRequest)]
        public StepDetail Step { get; set; }

        [Bind(Direction.ServerToClientFirstRequest)]
        public IEnumerable<StepListDetail> Steps { get; set; }

        [FromRoute("Step")]
        [Bind(Direction.ServerToClientFirstRequest)]
        public string StepMoniker { get; set; }

        public override async Task Load()
        {
            // get available languages
            var lesson = await workspace.LoadLesson(LessonMoniker);
            Languages = lesson.Variants.Where(v => v != LanguageMoniker)
                .Select(LanguageOption.Create)
                .ToList();

            // load step
            var variant = await workspace.LoadLessonVariant(LessonMoniker, LanguageMoniker);
            LessonName = variant.Name;
            var stepTasks = variant.Steps.Select(async s => await workspace.LoadStep(LessonMoniker, LanguageMoniker, s));
            var steps = await Task.WhenAll(stepTasks);
            Steps = steps.Select(s => new StepListDetail { Moniker = s.StepMoniker, Name = s.Name });
            var step = steps.Single(s => s.StepMoniker == StepMoniker);
            var index = variant.Steps.IndexOf(StepMoniker);
            Step = new StepDetail
            {
                Html = step.Text,
                Name = step.Name,
                PreviousStep = variant.Steps.ElementAtOrDefault(index - 1),
                NextStep = variant.Steps.ElementAtOrDefault(index + 1),
                HasCodeTask = step.CodeTaskPath != null,
                HasEmbeddedView = step.EmbeddedView != null,
                HasArchive = step.SolutionArchivePath != null
            };
            if (step.CodeTaskPath != null)
            {
                if (!Context.IsPostBack)
                {
                    CodeTask = new CodeTaskDetail();
                }
                CodeTask.SourcePath = step.CodeTaskPath;
                if (!Context.IsPostBack)
                {
                    await Reset();
                }
            }

            await base.Load();
        }

        public async Task Reset()
        {
            var script = await workspace.ContextLoad<CodeTask>(LessonMoniker, LanguageMoniker, CodeTask.SourcePath);
            CodeTask = new CodeTaskDetail
            {
                Code = await environment.ContextRead(LessonMoniker, LanguageMoniker, script.Unit.GetDefault()),
                CodeLanguage = script.Unit.GetValidatedLanguage()
            };
        }

        public async Task ShowSolution()
        {
            var script = await workspace.ContextLoad<CodeTask>(LessonMoniker, LanguageMoniker, CodeTask.SourcePath);
            CodeTask.Code = await environment.ContextRead(LessonMoniker, LanguageMoniker, script.Unit.GetCorrect());
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
                var script = await workspace.ContextLoad<CodeTask>(LessonMoniker, LanguageMoniker, CodeTask.SourcePath);
                var diagnostics = (await validator.Validate(script, CodeTask.Code))
                    .ToArray();
                CodeTask.IsCodeCorrect = !diagnostics.Any(d => d.Severity == CodeTaskDiagnosticSeverity.Error);
                CodeTask.Markers = diagnostics
                    .Select(GetMarker)
                    .OrderBy(m => (m.StartLineNumber, m.StartColumn))
                    .OrderBy(m => m.Severity)
                    .ToList();
            }
            catch (CodeTaskException)
            {
                CodeTask.Markers.Clear();
                CodeTask.Markers.Add(new MonacoMarker("An error occured during validation.", MonacoSeverity.Error, -1, -1, -1, -1));
            }
        }
    }
}