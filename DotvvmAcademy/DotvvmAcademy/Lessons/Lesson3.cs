using DotvvmAcademy.Services;
using Microsoft.AspNetCore.Hosting;

namespace DotvvmAcademy.Lessons
{
    public class Lesson3 : LessonBase
    {
        public Lesson3(IHostingEnvironment hostingEnvironment)
        {
            var lessonXmlRelativePath = @"Lessons/Lesson3.xml";
            var lessonProvider = new LessonUserInterfaceProvider(lessonXmlRelativePath, hostingEnvironment);
            Steps = lessonProvider.LessonSteps;
        }
    }
}