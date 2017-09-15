using DotVVM.Framework.ViewModel;
using System.Globalization;
using System.Threading.Tasks;

namespace DotvvmAcademy.ViewModels
{
    public abstract class DotvvmAcademyViewModelBase : DotvvmViewModelBase
    {
        [FromRoute("Language")]
        [Bind(Direction.None)]
        public CultureInfo Language { get; set; }

        public override Task Init()
        {
            Context.ChangeCurrentCulture(Language.TwoLetterISOLanguageName);
            return Task.CompletedTask;
        }
    }
}