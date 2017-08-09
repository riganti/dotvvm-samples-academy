using DotvvmAcademy.Services;
using Microsoft.AspNetCore.Hosting;

namespace DotvvmAcademy.Lessons
{
    public class LessonRu2 : LessonBase
    {
        public LessonRu2(IHostingEnvironment hostingEnvironment)
        {
            var XmlRelativePath = @"Lessons\ru\Lesson2.xml";
            var lessonProvider = new LessonUserInterfaceProvider(XmlRelativePath, hostingEnvironment);
            Steps = lessonProvider.LessonSteps;
        }
    }
}