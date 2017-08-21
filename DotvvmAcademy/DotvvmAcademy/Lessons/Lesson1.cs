using DotvvmAcademy.Services;
using Microsoft.AspNetCore.Hosting;

namespace DotvvmAcademy.Lessons
{
    public class Lesson1 : LessonBase
    {
        public Lesson1(IHostingEnvironment hostingEnvironment)
        {
            var lessonXmlRelativePath = @"Lessons/Lesson1.xml";
            var lessonProvider = new LessonUserInterfaceProvider(lessonXmlRelativePath, hostingEnvironment);
            Steps = lessonProvider.LessonSteps;
        }
    }
}