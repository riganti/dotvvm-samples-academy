using DotvvmAcademy.Lessons;
using DotvvmAcademy.Services;
using System.Globalization;

namespace DotvvmAcademy.DTO
{
    public class LessonDTO
    {
        public string CurrentCulture { get; set; }
        public string ButtonText
        {
            get
            {
                if (IsFinished)
                {
                    return LessonNames.ResourceManager.GetString("IsFinishedLessonBtnText", new CultureInfo(CurrentCulture));
                }
                if (IsVisited)
                {
                    return LessonNames.ResourceManager.GetString("IsVisitedLessonBtnText", new CultureInfo(CurrentCulture));
                }
                if (!IsCreated)
                {
                    return LessonNames.ResourceManager.GetString("IsCreatedLessonBtnText", new CultureInfo(CurrentCulture));
                }
                return LessonNames.ResourceManager.GetString("StartLessonBtnText", new CultureInfo(CurrentCulture));
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
        public string Description { get; set; }
    }
}