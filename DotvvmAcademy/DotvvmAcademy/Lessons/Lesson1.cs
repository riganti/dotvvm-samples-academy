using DotvvmAcademy.Services;

namespace DotvvmAcademy.Lessons
{
    public class Lesson1 : LessonBase
    {
        public Lesson1()
        {
            var lesson1XmlRelativePath = @"Lessons\Lesson1.xml";
            var lessonProvider = new LessonUserInterfaceProvider(lesson1XmlRelativePath);
            Steps = lessonProvider.LessonSteps;
           
        }
    }
}