using DotvvmAcademy.Services;
using Microsoft.AspNetCore.Hosting;

namespace DotvvmAcademy.Lessons
{
    public class LessonCs4 : LessonBase
    {
        public LessonCs4(IHostingEnvironment hostingEnvironment)
        {
            var lesson1XmlRelativePath = @"Lessons\cs\LessonCs4.xml";
            var lessonProvider = new LessonUserInterfaceProvider(lesson1XmlRelativePath, hostingEnvironment);
            Steps = lessonProvider.LessonSteps;
        }
    }
}