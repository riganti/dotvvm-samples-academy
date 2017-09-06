using DotVVM.Framework.ViewModel;
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

        [FromRoute("Language")]
        public string Language { get; set; }

        public List<LessonOverviewDto> Lessons { get; private set; }

        public override async Task Init()
        {
            if(string.IsNullOrEmpty(Language))
            {
                Language = "en";
            }
            Lessons = (await lessonFacade.GetOverviews(Language)).ToList();
        }
    }
}