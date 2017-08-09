using DotVVM.Framework.Hosting;
using DotvvmAcademy.DTO;
using DotvvmAcademy.Lessons;
using DotvvmAcademy.Services;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace DotvvmAcademy.ViewModels
{
    public class DefaultViewModel : SiteViewModel
    {
        public List<LessonDTO> Lessons { get; private set; }

        public string LessonName { get; set; }
        public override Task Load()
        {
            SetCurrentLanguage();
            LessonName = LessonNames.ResourceManager.GetString("LessonText", CultureInfo.CurrentCulture);

            OnlineCourseText = LessonNames.ResourceManager.GetString("OnlineCourseText", CultureInfo.CurrentCulture);
            SamplesText = LessonNames.ResourceManager.GetString("SamplesText", CultureInfo.CurrentCulture);
            DocumentationText = LessonNames.ResourceManager.GetString("DocumentationText", CultureInfo.CurrentCulture);
            TutorialsText = LessonNames.ResourceManager.GetString("TutorialsText", CultureInfo.CurrentCulture);

            var storage = new LessonProgressStorage(Context.GetAspNetCoreContext());
            Lessons = new List<LessonDTO>
            {
                new LessonDTO
                {
                    Number = 1,
                    LastStep = storage.GetLessonLastStep(1),
                    Title = LessonNames.ResourceManager.GetString("Lesson1", CultureInfo.CurrentCulture),
                    ImageUrl = "/img/basics.png",
                    CurrentCulture = Context.GetCurrentCulture().Name
                },
                new LessonDTO
                {
                    Number = 2,
                    LastStep = storage.GetLessonLastStep(2),
                    Title = LessonNames.ResourceManager.GetString("Lesson2", CultureInfo.CurrentCulture),
                    ImageUrl = "/img/elementary.png",
                    CurrentCulture = Context.GetCurrentCulture().Name
                },
                new LessonDTO
                {
                    Number = 3,
                    LastStep = storage.GetLessonLastStep(3),
                    Title = LessonNames.ResourceManager.GetString("Lesson3", CultureInfo.CurrentCulture),
                    ImageUrl = "/img/intermediate.png",
                    CurrentCulture = Context.GetCurrentCulture().Name
                },
                new LessonDTO
                {
                    Number = 4,
                    LastStep = storage.GetLessonLastStep(4),
                    Title = LessonNames.ResourceManager.GetString("Lesson4", CultureInfo.CurrentCulture),
                    ImageUrl = "/img/advanced.png",
                    CurrentCulture = Context.GetCurrentCulture().Name
                },
                //new LessonDTO
                //{
                //    Number = 5,
                //    LastStep = storage.GetLessonLastStep(5),
                //    Title = "Understand <code>GridView</code> and loading data from <code>IQueryable</code>.",
                //    ImageUrl = "/img/professional.png"
                //}
            };
            return base.Load();
        }
    }
}