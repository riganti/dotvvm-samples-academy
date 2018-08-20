using DotVVM.Framework.ViewModel;
using DotvvmAcademy.CourseFormat;
using DotvvmAcademy.Validation.Unit;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.Web.ViewModels
{
    public class StepViewModel : SiteViewModel
    {
        private readonly StepRenderer stepRenderer;
        private readonly ValidationScriptRunner runner;
        private readonly CodeTaskValidator validator;
        private readonly CourseWorkspace workspace;
        private Lesson lesson;
        private RenderedStep renderedStep;
        private Step step;

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

        [Bind(Direction.ServerToClient)]
        public string Name { get; set; }

        public string Code { get; set; }

        [Bind(Direction.None)]
        public string CodeLanguage { get; set; }

        [Bind(Direction.ServerToClient)]
        public List<CodeTaskDiagnostic> Diagnostics { get; set; }

        [Bind(Direction.ServerToClient)]
        public bool HasCodeTask { get; set; }

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
            lesson = await workspace.LoadLesson(LanguageMoniker, Lesson);
            step = await workspace.LoadStep(LanguageMoniker, Lesson, Step);
            renderedStep = stepRenderer.Render(step);
            SetButtonProperties();
            await SetEditorProperties();
            Name = renderedStep.Name;
            Text = renderedStep.Html;
            await base.Load();
        }

        public async Task Validate()
        {
            var unit = await runner.Run(renderedStep.CodeTaskPath);
            Diagnostics = (await validator.Validate(unit, Code)).ToList();
        }

        protected override async Task<IEnumerable<string>> GetAvailableLanguageMonikers()
        {
            var root = await workspace.LoadRoot();
            var builder = ImmutableArray.CreateBuilder<string>();
            foreach (var variant in root.Variants)
            {
                var step = await workspace.LoadStep(variant, Lesson, Step);
                if (step != null)
                {
                    builder.Add(variant);
                }
            }
            return builder.ToImmutable();
        }

        private void SetButtonProperties()
        {
            var index = lesson.Steps.IndexOf(Step);
            IsPreviousVisible = index > 0;
            IsNextVisible = index < lesson.Steps.Length - 1;
            if (IsPreviousVisible)
            {
                PreviousStep = lesson.Steps[index - 1];
            }
            if (IsNextVisible)
            {
                NextStep = lesson.Steps[index + 1];
            }
        }

        private async Task SetEditorProperties()
        {
            HasCodeTask = renderedStep.CodeTaskPath != null;
            if (!Context.IsPostBack && HasCodeTask)
            {
                var unit = await runner.Run(renderedStep.CodeTaskPath);
                var defaultCodePath = unit.Provider.GetRequiredService<CodeTaskConfiguration>().DefaultCodePath;
                Code = defaultCodePath == null ? string.Empty : (await workspace.Load<Resource>(defaultCodePath)).Text;
                CodeLanguage = unit.GetValidatedLanguage();
            }
        }
    }
}