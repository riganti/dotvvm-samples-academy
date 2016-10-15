using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using DotvvmAcademy.Lessons;
using DotvvmAcademy.DTO;
using DotvvmAcademy.Services;
using DotVVM.Framework.Hosting;

namespace DotvvmAcademy.ViewModels
{
    public class DefaultViewModel : SiteViewModel
    {

        public List<LessonDTO> Lessons { get; private set; }

        public override Task Init()
        {
            var storage = new LessonProgressStorage(Context.GetAspNetCoreContext());
            Lessons = new List<LessonDTO>()
            {
                new LessonDTO()
                {
                    Number = 1,
                    LastStep = storage.GetLessonLastStep(1),
                    Title = "Understand basic principles of DotVVM and MVVM pattern.",
                    ImageUrl = "/img/basics.png"
                },
                new LessonDTO()
                {
                    Number = 2,
                    LastStep = storage.GetLessonLastStep(2),
                    Title = "Learn how to use the <code>Repeater</code> control and how to work with collections.",
                    ImageUrl = "/img/elementary.png"
                },
                new LessonDTO()
                {
                    Number = 3,
                    LastStep = storage.GetLessonLastStep(3),
                    Title = "Try out working with advanced form controls like <code>ComboBox</code> and <code>RadioButton</code>.",
                    ImageUrl = "/img/intermediate.png"
                },
                new LessonDTO()
                {
                    Number = 4,
                    LastStep = storage.GetLessonLastStep(4),
                    Title = "Learn how validation works and <code>DataContext</code> works.",
                    ImageUrl = "/img/advanced.png"
                },
                new LessonDTO()
                {
                    Number = 5,
                    LastStep = storage.GetLessonLastStep(5),
                    Title = "Understand <code>GridView</code> and loading data from <code>IQueryable</code>.",
                    ImageUrl = "/img/professional.png"
                }
            };

            return base.Init();
        }

    }
}
