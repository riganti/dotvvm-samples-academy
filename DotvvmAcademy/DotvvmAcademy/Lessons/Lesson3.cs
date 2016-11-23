using DotvvmAcademy.Services;

namespace DotvvmAcademy.Lessons
{
    public class Lesson3 : LessonBase
    {
        public Lesson3()
        {
            var lesson1XmlRelativePath = @"Lessons\Lesson3.xml";
            var lessonProvider = new LessonUserInterfaceProvider(lesson1XmlRelativePath);
            Steps = lessonProvider.LessonSteps;
        }
    }
}