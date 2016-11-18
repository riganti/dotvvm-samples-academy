using DotvvmAcademy.Services;

namespace DotvvmAcademy.Lessons
{
    public class Lesson1 : LessonBase
    {
        public Lesson1()
        {
            var lesson1RelativePath = @"Lessons\Lesson1.xml";
            var lessonProvider = new LessonUserInterfaceProvider(lesson1RelativePath);
            Steps = lessonProvider.LessonSteps;
        }
    }
}