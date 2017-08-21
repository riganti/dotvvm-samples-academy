using DotvvmAcademy.Services;
using Microsoft.AspNetCore.Hosting;

namespace DotvvmAcademy.Lessons
{
    public class LessonCs1 : LessonBase
    {
        public LessonCs1(IHostingEnvironment hostingEnvironment)
        {
            var XmlRelativePath = @"Lessons/cs/Lesson1.xml";
            var lessonProvider = new LessonUserInterfaceProvider(XmlRelativePath, hostingEnvironment);
            Steps = lessonProvider.LessonSteps;
        }
    }
}