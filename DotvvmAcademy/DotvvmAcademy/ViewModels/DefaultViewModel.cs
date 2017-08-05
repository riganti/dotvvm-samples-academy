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

        public override Task Init()
        {
            GetCurrentLanguage();

            var storage = new LessonProgressStorage(Context.GetAspNetCoreContext());
            Lessons = new List<LessonDTO>
            {
                new LessonDTO
                {
                    Number = 1,
                    LastStep = storage.GetLessonLastStep(1),
                    Title = LessonNames.ResourceManager.GetString("Lesson1", CultureInfo.CurrentCulture),
                    ImageUrl = "/img/basics.png"
                },
                new LessonDTO
                {
                    Number = 2,
                    LastStep = storage.GetLessonLastStep(2),
                    Title = LessonNames.ResourceManager.GetString("Lesson2", CultureInfo.CurrentCulture),
                    ImageUrl = "/img/elementary.png"
                },
                new LessonDTO
                {
                    Number = 3,
                    LastStep = storage.GetLessonLastStep(3),
                    Title = LessonNames.ResourceManager.GetString("Lesson3", CultureInfo.CurrentCulture),
                    ImageUrl = "/img/intermediate.png"
                },
                new LessonDTO
                {
                    Number = 4,
                    LastStep = storage.GetLessonLastStep(4),
                    Title = LessonNames.ResourceManager.GetString("Lesson4", CultureInfo.CurrentCulture),
                    ImageUrl = "/img/advanced.png"
                },
                //new LessonDTO
                //{
                //    Number = 5,
                //    LastStep = storage.GetLessonLastStep(5),
                //    Title = "Understand <code>GridView</code> and loading data from <code>IQueryable</code>.",
                //    ImageUrl = "/img/professional.png"
                //}
            };
            return base.Init();
        }
    }
}