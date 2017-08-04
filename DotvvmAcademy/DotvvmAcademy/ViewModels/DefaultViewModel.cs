using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel;
using DotvvmAcademy.BL.DTO;
using DotvvmAcademy.BL.Facades;
using DotvvmAcademy.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.ViewModels
{
    public class DefaultViewModel : DotvvmAcademyViewModelBase
    {
        private LessonFacade lessonFacade;

        public DefaultViewModel(LessonFacade lessonFacade)
        {
            this.lessonFacade = lessonFacade;
        }

        public List<LessonDTO> Lessons { get; private set; }

        public List<int> LastStepIndices { get; private set; } = new List<int>();

        public override Task Init()
        {
            Lessons = lessonFacade.GetAllLessons(language: "en").ToList();

            var storage = new LessonProgressStorage(Context.GetAspNetCoreContext());
            foreach(var lesson in Lessons)
            {
                LastStepIndices.Add(storage.GetLessonLastStep(lesson.Index + 1));
            }

            return base.Init();
        }
    }
}