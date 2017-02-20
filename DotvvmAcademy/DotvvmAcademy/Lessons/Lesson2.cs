using DotvvmAcademy.Services;
using Microsoft.AspNetCore.Hosting;

namespace DotvvmAcademy.Lessons
{
    public class Lesson2 : LessonBase
    {
        public Lesson2(IHostingEnvironment hostingEnvironment)
        {
            var lesson2RelativePath = @"Lessons\Lesson2.xml";
            var lessonProvider = new LessonUserInterfaceProvider(lesson2RelativePath, hostingEnvironment);
            Steps = lessonProvider.LessonSteps;
        }
    }
}