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
        public StepDetail Step { get; set; }

        [Bind(Direction.ServerToClientFirstRequest)]
        public IEnumerable<StepListDetail> Steps { get; set; }

        [FromRoute("Step")]
        [Bind(Direction.ServerToClientFirstRequest)]
        public string StepMoniker { get; set; }

        public override async Task Load()
        {
            var lesson = await workspace.LoadLesson(LanguageMoniker, LessonMoniker);
            var steps = await Task.WhenAll(lesson.Steps.Select(async s => await workspace.LoadStep(LanguageMoniker, LessonMoniker, s)));
            Steps = steps.Select(s => new StepListDetail { Moniker = s.Moniker, Name = s.Name });
            var step = await workspace.LoadStep(LanguageMoniker, LessonMoniker, StepMoniker);
            var index = lesson.Steps.IndexOf(StepMoniker);
            Step = new StepDetail
            {
                Html = step.Text,
                Name = step.Name,
                PreviousStep = lesson.Steps.ElementAtOrDefault(index - 1),
                NextStep = lesson.Steps.ElementAtOrDefault(index + 1),
                HasCodeTask = step.CodeTask != null,
                HasEmbeddedView = step.EmbeddedView != null
            };
            if (step.CodeTask != null)
            {
                if (!Context.IsPostBack)
                {
                    CodeTask = new CodeTaskDetail();
                }
                CodeTask.ValidationScriptPath = step.CodeTask.ValidationScriptPath;
                if (!Context.IsPostBack)
                {
                    await Reset();
                }
            }

            await base.Load();
        }

        public async Task Reset()
        {
            var script = await workspace.Require<ValidationScript>(CodeTask.ValidationScriptPath);
            CodeTask = new CodeTaskDetail
            {
                Code = await environment.Read(script.Unit.GetCodeTaskOptions().DefaultCodePath),
                CodeLanguage = script.Unit.GetValidatedLanguage()
            };
        }

        public async Task ShowSolution()
        {
            var script = await workspace.Require<ValidationScript>(CodeTask.ValidationScriptPath);
            var correctCodePath = script.Unit.GetCodeTaskOptions().CorrectCodePath;
            CodeTask.Code = await environment.Read(correctCodePath);
        }

        public async Task Validate()
        {
            var script = await workspace.Require<ValidationScript>(CodeTask.ValidationScriptPath);
            var converter = new PositionConverter(CodeTask.Code);
            var diagnostics = await validator.Validate(script.Unit, CodeTask.Code);
            if (diagnostics.Length == 0)
            {
                Context.RedirectToRoute("Step", new { Step = Step.NextStep });
            }

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
                    (startLineNumber, startColumn) = converter.ToCoords(diagnostic.Start);
                    (endLineNumber, endColumn) = converter.ToCoords(diagnostic.End - 1);
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

            CodeTask.Markers = diagnostics
                .Select(GetMarker)
                .OrderBy(m => (m.StartLineNumber, m.StartColumn))
                .OrderBy(m => m.Severity)
                .ToList();
        }

        protected override async Task<IEnumerable<string>> GetAvailableLanguageMonikers()
        {
            var root = await workspace.LoadRoot();
            var builder = ImmutableArray.CreateBuilder<string>();
            foreach (var variant in root.Variants)
            {
                var step = await workspace.LoadStep(variant, LessonMoniker, StepMoniker);
                if (step != null)
                {
                    builder.Add(variant);
                }
            }
            return builder.ToImmutable();
        }
    }
}