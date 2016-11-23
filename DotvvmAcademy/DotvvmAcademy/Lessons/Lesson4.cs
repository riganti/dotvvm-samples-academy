using DotvvmAcademy.Services;

namespace DotvvmAcademy.Lessons
{
    public class Lesson4 : LessonBase
    {
        public Lesson4()
        {
            var lesson1XmlRelativePath = @"Lessons\Lesson4.xml";
            var lessonProvider = new LessonUserInterfaceProvider(lesson1XmlRelativePath);
            Steps = lessonProvider.LessonSteps;
        }
    }
}