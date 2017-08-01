using DotvvmAcademy.Services;

namespace DotvvmAcademy.DTO
{
    public class LessonDTO
    {
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

        public string ImageUrl { get; set; }

        //todo
        public bool IsCreated => (Number == 1) || (Number == 2) || (Number == 3) || (Number == 4);

        public bool IsFinished => LastStep == LessonProgressStorage.FinishedLessonStepNumber;

        public bool IsVisited => LastStep > 1;

        public int LastStep { get; set; }

        public int Number { get; set; }

        public int StepToOpen => IsFinished ? 1 : LastStep;

        public string Title { get; set; }
    }
}