using DotvvmAcademy.Services;
using Microsoft.AspNetCore.Hosting;

namespace DotvvmAcademy.Lessons
{
    public class Lesson4 : LessonBase
    {
        public Lesson4(IHostingEnvironment hostingEnvironment)
        {
            var lesson1XmlRelativePath = @"Lessons\Lesson4.xml";
            var lessonProvider = new LessonUserInterfaceProvider(lesson1XmlRelativePath, hostingEnvironment);
            Steps = lessonProvider.LessonSteps;
        }
    }
}