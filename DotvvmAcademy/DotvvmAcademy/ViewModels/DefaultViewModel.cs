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
            LessonName = LessonNames.ResourceManager.GetString("LessonText", CultureInfo.CurrentCulture);

            TutorialsText = LessonNames.ResourceManager.GetString("TutorialsText", CultureInfo.CurrentCulture);

            var storage = new LessonProgressStorage(Context.GetAspNetCoreContext());
            Lessons = new List<LessonDTO>
            {
                new LessonDTO
                {
                    Number = 1,
                    LastStep = storage.GetLessonLastStep(1),
                    Title = LessonNames.ResourceManager.GetString("Lesson1_Title", CultureInfo.CurrentCulture),
                    Description = LessonNames.ResourceManager.GetString("Lesson1_Desc", CultureInfo.CurrentCulture),
                    ImageUrl = "images/Icons/ico-lesson-1.svg",
                    CurrentCulture = CultureInfo.CurrentCulture.Name
                },
                new LessonDTO
                {
                    Number = 2,
                    LastStep = storage.GetLessonLastStep(2),
                    Title = LessonNames.ResourceManager.GetString("Lesson2_Title", CultureInfo.CurrentCulture),
                    Description = LessonNames.ResourceManager.GetString("Lesson2_Desc", CultureInfo.CurrentCulture),
                    ImageUrl = "images/Icons/ico-lesson-2.svg",
                    CurrentCulture = CultureInfo.CurrentCulture.Name
                },
                new LessonDTO
                {
                    Number = 3,
                    LastStep = storage.GetLessonLastStep(3),
                    Title = LessonNames.ResourceManager.GetString("Lesson3_Title", CultureInfo.CurrentCulture),
                    Description = LessonNames.ResourceManager.GetString("Lesson3_Desc", CultureInfo.CurrentCulture),
                    ImageUrl = "images/Icons/ico-lesson-3.svg",
                    CurrentCulture = CultureInfo.CurrentCulture.Name
                },
                new LessonDTO
                {
                    Number = 4,
                    LastStep = storage.GetLessonLastStep(4),
                    Title = LessonNames.ResourceManager.GetString("Lesson4_Title", CultureInfo.CurrentCulture),
                    Description = LessonNames.ResourceManager.GetString("Lesson4_Desc", CultureInfo.CurrentCulture),
                    ImageUrl = "images/Icons/ico-lesson-4.svg",
                    CurrentCulture = CultureInfo.CurrentCulture.Name
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