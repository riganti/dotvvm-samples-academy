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
                    Title = "DotVVM Basics"
                }
            };

            return base.Init();
        }

    }
}
