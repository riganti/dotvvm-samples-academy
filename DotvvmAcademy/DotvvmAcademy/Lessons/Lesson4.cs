using DotvvmAcademy.Services;
using Microsoft.AspNetCore.Hosting;

namespace DotvvmAcademy.Lessons
{
    public class Lesson4 : LessonBase
    {
        public Lesson4(IHostingEnvironment hostingEnvironment)
        {
            var lessonXmlRelativePath = @"Lessons/Lesson4.xml";
            var lessonProvider = new LessonUserInterfaceProvider(lessonXmlRelativePath, hostingEnvironment);
            Steps = lessonProvider.LessonSteps;
        }
    }
}