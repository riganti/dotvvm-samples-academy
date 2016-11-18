using DotvvmAcademy.Services;

namespace DotvvmAcademy.Lessons
{
    public class Lesson2 : LessonBase
    {
        public Lesson2()
        {
            var lesson2RelativePath = @"Lessons\Lesson2.xml";
            var lessonProvider = new LessonUserInterfaceProvider(lesson2RelativePath);
            Steps = lessonProvider.LessonSteps;
        }
    }
}