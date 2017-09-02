using DotvvmAcademy.BL.DTO;
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

        public List<LessonDTO> Lessons { get; private set; }

        public override Task Init()
        {
            Lessons = lessonFacade.GetAllLessons(language: "en").ToList();

            return base.Init();
        }
    }
}