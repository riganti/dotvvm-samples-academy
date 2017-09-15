using DotvvmAcademy.BL.Dtos;
using DotvvmAcademy.BL.Facades;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.ViewModels
{
    public class LessonsOverviewViewModel : DotvvmAcademyViewModelBase
    {
        private LessonFacade lessonFacade;

        public LessonsOverviewViewModel(LessonFacade lessonFacade)
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