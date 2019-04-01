using DotVVM.Framework.ViewModel;
using DotvvmAcademy.CourseFormat;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.Web.Pages.Test
{
    public class TestViewModel : DotvvmViewModelBase
    {
        private readonly CourseWorkspace workspace;

        public TestViewModel(CourseWorkspace workspace)
        {
            this.workspace = workspace;
        }

        public List<CodeTaskDiagnostic> Diagnostics { get; set; }

        [FromRoute("LessonMoniker")]
        public string LessonMoniker { get; set; }

        [FromRoute("VariantMoniker")]
        public string VariantMoniker { get; set; }

        [FromRoute("StepMoniker")]
        public string StepMoniker { get; set; }

        public override async Task Load()
        {
            var step = await workspace.LoadStep(LessonMoniker, VariantMoniker, StepMoniker);
            var diagnostics = await workspace.ValidateStep(step, "");
            Diagnostics = diagnostics.ToList();
        }
    }
}