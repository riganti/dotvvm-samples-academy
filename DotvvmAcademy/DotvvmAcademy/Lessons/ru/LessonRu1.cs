using DotvvmAcademy.Services;
using Microsoft.AspNetCore.Hosting;

namespace DotvvmAcademy.Lessons
{
    public class LessonRu1 : LessonBase
    {
        public LessonRu1(IHostingEnvironment hostingEnvironment)
        {
            var XmlRelativePath = @"Lessons/ru/Lesson1.xml";
            var lessonProvider = new LessonUserInterfaceProvider(XmlRelativePath, hostingEnvironment);
            Steps = lessonProvider.LessonSteps;
        }
    }
}