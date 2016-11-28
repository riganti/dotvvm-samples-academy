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
        //todo 
        public bool IsCreated => (Number == 1) || (Number == 2) || (Number == 3);

        public string ButtonText
        {
            get
            {
                if (IsFinished)
                {
                    return "Repeat Lesson";
                }
                if (IsVisited)
                {
                    return "Continue";
                }
                if (!IsCreated)
                {
                    return "Coming Soon";
                }
                return "Start Lesson";
            }
        }

        public int StepToOpen => IsFinished ? 1 : LastStep;
    }
}