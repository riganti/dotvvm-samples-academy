using DotvvmAcademy.BL.Dtos;
using DotvvmAcademy.BL.Facades;
using DotvvmAcademy.Resources;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.ViewModels
{
    public class IndexViewModel : DotvvmAcademyViewModelBase
    {
        private LessonFacade lessonFacade;

        public IndexViewModel(LessonFacade lessonFacade, IStringLocalizer<UIResources> localizer)
             : base(localizer)
        {
            this.lessonFacade = lessonFacade;
        }

        public List<LessonOverviewDto> Lessons { get; private set; }

        public override async Task Init()
        {
            await base.Init();
            Lessons = (await lessonFacade.GetOverviews(Language.TwoLetterISOLanguageName)).ToList();
        }
    }
}