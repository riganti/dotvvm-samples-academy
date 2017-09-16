using DotvvmAcademy.Services;
using Microsoft.AspNetCore.Hosting;

namespace DotvvmAcademy.Lessons
{
    public class LessonCs4 : LessonBase
    {
        public LessonCs4(IHostingEnvironment hostingEnvironment)
        {
            var XmlRelativePath = @"Lessons/cs/Lesson4.xml";
            var lessonProvider = new LessonUserInterfaceProvider(XmlRelativePath, hostingEnvironment);
            Steps = lessonProvider.LessonSteps;
        }
    }
}