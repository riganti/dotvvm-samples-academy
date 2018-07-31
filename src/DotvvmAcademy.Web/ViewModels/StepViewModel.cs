using DotVVM.Framework.ViewModel;
using DotvvmAcademy.CourseFormat;
using Markdig;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.Web.ViewModels
{
    public class StepViewModel : SiteViewModel
    {
        private readonly MarkdownExtractor extractor;
        private readonly CodeTaskValidator validator;
        private readonly CourseWorkspace workspace;
        private Step step;

        public StepViewModel(CourseWorkspace workspace, MarkdownExtractor extractor, CodeTaskValidator validator)
        {
            this.workspace = workspace;
            this.extractor = extractor;
            this.validator = validator;
        }

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
            var lesson = await workspace.LoadLesson(Language, Lesson);
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
            step = await workspace.LoadStep(Language, Lesson, Step);
            HasCodeTask = step.CodeTask != null;
            Text = Markdown.ToHtml(step.Text);
            if (!Context.IsPostBack && step.CodeTask != null)
            {
                var codeTask = await workspace.LoadCodeTask(Language, Lesson, Step, step.CodeTask);
                var unit = await validator.GetUnit(codeTask);
                var defaultCodePath = unit.Provider
                    .GetRequiredService<SourcePathStorage>()
                    .Get("DefaultCode");
                var defaultCodeResource = await workspace.Load<Resource>(defaultCodePath);
                Code = defaultCodeResource?.Text ?? null;
                CodeLanguage = codeTask.CodeLanguage;
            }
            await base.Load();
        }

        public async Task Validate()
        {
            var codeTask = await workspace.LoadCodeTask(Language, Lesson, Step, step.CodeTask);
            var unit = await validator.GetUnit(codeTask);
            Diagnostics = (await validator.Validate(unit, Code)).ToList();
        }

        protected override async Task<IEnumerable<string>> GetAvailableLanguages()
        {
            var root = await workspace.LoadRoot();
            var builder = ImmutableArray.CreateBuilder<string>();
            foreach(var variant in root.Variants)
            {
                var step = await workspace.LoadStep(variant, Lesson, Step);
                if (step != null)
                {
                    builder.Add(variant);
                }
            }
            return builder.ToImmutable();
        }
    }
}