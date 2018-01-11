using DotVVM.Framework.ViewModel;
using DotvvmAcademy.Resources;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Threading.Tasks;

namespace DotvvmAcademy.ViewModels
{
    public abstract class DotvvmAcademyViewModelBase : DotvvmViewModelBase
    {
        public DotvvmAcademyViewModelBase(IStringLocalizer<UIResources> localizer)
        {
            Localizer = localizer;
        }

        [FromRoute("Language")]
        [Bind(Direction.None)]
        public CultureInfo Language { get; set; }

        public override Task Init()
        {
            Context.ChangeCurrentCulture(Language.TwoLetterISOLanguageName);
            return Task.CompletedTask;
        }

        [Bind(Direction.None)]
        public IStringLocalizer<UIResources> Localizer { get; }

        public string OnlineCourse => UIResources.OnlineCourse;

        public string Documentation => UIResources.Documentation;

        public string Samples => UIResources.Samples;

        public string Tutorials => UIResources.Tutorials;
    }
}