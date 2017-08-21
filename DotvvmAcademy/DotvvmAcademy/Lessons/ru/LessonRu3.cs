using DotvvmAcademy.Services;
using Microsoft.AspNetCore.Hosting;

namespace DotvvmAcademy.Lessons
{
    public class LessonRu3 : LessonBase
    {
        public LessonRu3(IHostingEnvironment hostingEnvironment)
        {
            var XmlRelativePath = @"Lessons/ru/Lesson3.xml";
            var lessonProvider = new LessonUserInterfaceProvider(XmlRelativePath, hostingEnvironment);
            Steps = lessonProvider.LessonSteps;
        }
    }
}