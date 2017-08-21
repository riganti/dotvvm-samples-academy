using DotvvmAcademy.Services;
using Microsoft.AspNetCore.Hosting;

namespace DotvvmAcademy.Lessons
{
    public class LessonCs2 : LessonBase
    {
        public LessonCs2(IHostingEnvironment hostingEnvironment)
        {
            var XmlRelativePath = @"Lessons/cs/Lesson2.xml";
            var lessonProvider = new LessonUserInterfaceProvider(XmlRelativePath, hostingEnvironment);
            Steps = lessonProvider.LessonSteps;
        }
    }
}