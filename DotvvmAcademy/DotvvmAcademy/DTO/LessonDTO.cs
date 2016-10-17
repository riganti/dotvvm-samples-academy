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

        public string ButtonText => IsFinished ? "Repeat Lesson" : (IsVisited ? "Continue" : "Start Lesson");

        public int StepToOpen => IsFinished ? 1 : LastStep;

    }
}
