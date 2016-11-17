using DotvvmAcademy.Services;

namespace DotvvmAcademy.Lessons
{
    public class Lesson1 : LessonBase
    {
        public Lesson1()
        {
            var lesson1RelativePath = @"Lessons\Lesson1.xml";
            //todo lessonbase --
            var lessonProvider = new LessonUserInterfaceProvider(this, lesson1RelativePath);
            Steps = lessonProvider.LessonSteps;
        }
    }
}