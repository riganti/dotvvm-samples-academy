using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotvvmAcademy.Services;

namespace DotvvmAcademy.DTO
{
    public class LessonDTO
    {

        public int Number { get; set; }

        public int LastStep { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public bool IsVisited => LastStep > 1;

        public bool IsFinished => LastStep == LessonProgressStorage.FinishedLessonStepNumber;

        public bool IsCreated => Number == 1 || Number == 2;

        public string ButtonText
        {
            get
            {
                if (IsFinished)
                {
                    return "Repeat Lesson";
                }
                else if (IsVisited)
                {
                    return "Continue";
                }
                else if (!IsCreated)
                {
                    return "Coming Soon";
                }
                else return "Start Lesson";
            }
        }

        public int StepToOpen => IsFinished ? 1 : LastStep;

    }
}
