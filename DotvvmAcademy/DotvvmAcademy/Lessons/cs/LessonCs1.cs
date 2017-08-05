using DotvvmAcademy.Services;
using Microsoft.AspNetCore.Hosting;

namespace DotvvmAcademy.Lessons
{
    public class LessonCs1 : LessonBase
    {
        public LessonCs1(IHostingEnvironment hostingEnvironment)
        {
            var lesson1XmlRelativePath = @"Lessons\cs\LessonCs1.xml";
            var lessonProvider = new LessonUserInterfaceProvider(lesson1XmlRelativePath, hostingEnvironment);
            Steps = lessonProvider.LessonSteps;
        }
    }
}