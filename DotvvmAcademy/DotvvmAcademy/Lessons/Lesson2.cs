using DotvvmAcademy.Services;
using Microsoft.AspNetCore.Hosting;

namespace DotvvmAcademy.Lessons
{
    public class Lesson2 : LessonBase
    {
        public Lesson2(IHostingEnvironment hostingEnvironment)
        {
            var lessonXmlRelativePath = @"Lessons/Lesson2.xml";
            var lessonProvider = new LessonUserInterfaceProvider(lessonXmlRelativePath, hostingEnvironment);
            Steps = lessonProvider.LessonSteps;
        }
    }
}