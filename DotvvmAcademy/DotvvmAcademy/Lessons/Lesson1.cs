using DotvvmAcademy.Services;
using Microsoft.AspNetCore.Hosting;

namespace DotvvmAcademy.Lessons
{
    public class Lesson1 : LessonBase
    {
        public Lesson1(IHostingEnvironment hostingEnvironment)
        {
            var lesson1XmlRelativePath = @"Lessons\Lesson1.xml";
            var lessonProvider = new LessonUserInterfaceProvider(lesson1XmlRelativePath, hostingEnvironment);
            Steps = lessonProvider.LessonSteps;
           
        }
    }
}