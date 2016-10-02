using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using DotvvmAcademy.Steps;
using DotvvmAcademy.Lessons;

namespace DotvvmAcademy.ViewModels
{
	public class LessonViewModel : SiteViewModel
	{

        public StepBase Step { get; set; }
        public string ErrorMessage { get; private set; }

        private LessonBase lesson;
        private int lessonNumber;
        private int stepNumber;

        public override Task Init()
        {
            LoadStep();

            return base.Init();
        }

        private void LoadStep()
        {
            lessonNumber = Convert.ToInt32(Context.Parameters["Lesson"]);
            stepNumber = Convert.ToInt32(Context.Parameters["Step"]);

            if (lessonNumber == 1)
            {
                lesson = new Lesson1();
            }
            else
            {
                throw new NotSupportedException();
            }
            Step = lesson.GetAllSteps()[stepNumber - 1];
        }

        public void Continue()
        {
            ErrorMessage = Step.ErrorMessage;

            if (string.IsNullOrEmpty(ErrorMessage))
            {
                if (stepNumber < lesson.GetAllSteps().Length)
                {
                    Context.RedirectToRoute("Lesson", new { Step = stepNumber + 1 });
                }
                else
                {
                    Context.RedirectToRoute("Default");
                }
            }
        }
    }
}

