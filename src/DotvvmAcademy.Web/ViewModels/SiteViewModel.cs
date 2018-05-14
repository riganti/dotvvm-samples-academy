using DotVVM.Framework.ViewModel;

namespace DotvvmAcademy.Web.ViewModels
{
    public class SiteViewModel : DotvvmViewModelBase
    {
        public string DocumentationText { get; set; }

        public string OnlineCourseText { get; set; }

        public string SamplesText { get; set; }

        public string TutorialsText { get; set; }
    }
}