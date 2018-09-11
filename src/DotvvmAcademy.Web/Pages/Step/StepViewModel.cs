using DotVVM.Framework.ViewModel;
using DotvvmAcademy.CourseFormat;
using DotvvmAcademy.Meta;
using DotvvmAcademy.Validation.Unit;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.Web.Pages.Step
{
    public class StepViewModel : SiteViewModel
    {
        private readonly ValidationScriptRunner runner;
        private readonly StepRenderer stepRenderer;
        private readonly CodeTaskValidator validator;
        private readonly CourseWorkspace workspace;
        private RenderedStep renderedStep;

        public StepViewModel(
            CourseWorkspace workspace,
            CodeTaskValidator validator,
            StepRenderer stepRenderer,
            ValidationScriptRunner runner)
        {
            this.workspace = workspace;
            this.validator = validator;
            this.stepRenderer = stepRenderer;
            this.runner = runner;
        }

        public CodeTaskDetail CodeTask { get; set; }

        [FromRoute("Lesson")]
        public string LessonMoniker { get; set; }

        [Bind(Direction.ServerToClientFirstRequest)]
        public StepDetail Step { get; set; }

        [FromRoute("Step"), Bind(Direction.None)]
        public string StepMoniker { get; set; }

        public override async Task Load()
        {
            var lesson = await workspace.LoadLesson(LanguageMoniker, LessonMoniker);
            var step = await workspace.LoadStep(LanguageMoniker, LessonMoniker, StepMoniker);
            var index = lesson.Steps.IndexOf(StepMoniker);
            renderedStep = stepRenderer.Render(step);
            Step = new StepDetail
            {
                Html = renderedStep.Html,
                Name = renderedStep.Name,
                PreviousStep = lesson.Steps.ElementAtOrDefault(index - 1),
                NextStep = lesson.Steps.ElementAtOrDefault(index + 1)
            };

            if (!Context.IsPostBack && !string.IsNullOrEmpty(renderedStep.CodeTaskPath))
            {
                await Reset();
            }
            await base.Load();
        }

        public async Task Reset()
        {
            var unit = await runner.Run(renderedStep.CodeTaskPath);
            var defaultCodePath = unit.Provider.GetRequiredService<CodeTaskOptions>().DefaultCodePath;
            CodeTask = new CodeTaskDetail
            {
                Code = (await workspace.Load<Resource>(defaultCodePath)).Text,
                CodeLanguage = unit.GetValidatedLanguage()
            };
        }

        public async Task ShowSolution()
        {
            var unit = await runner.Run(renderedStep.CodeTaskPath);
            var correctCodePath = unit.Provider.GetRequiredService<CodeTaskOptions>().CorrectCodePath;
            CodeTask.Code = (await workspace.Load<Resource>(correctCodePath)).Text;
        }

        public async Task Validate()
        {
            var unit = await runner.Run(renderedStep.CodeTaskPath);
            var converter = new PositionConverter(CodeTask.Code);
            var diagnostics = await validator.Validate(unit, CodeTask.Code);

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