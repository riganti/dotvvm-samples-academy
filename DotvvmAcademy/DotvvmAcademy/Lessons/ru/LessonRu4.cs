using DotvvmAcademy.Services;
using Microsoft.AspNetCore.Hosting;

namespace DotvvmAcademy.Lessons
{
    public class LessonRu4 : LessonBase
    {
        public LessonRu4(IHostingEnvironment hostingEnvironment)
        {
            var XmlRelativePath = @"Lessons\cs\Lesson4.xml";
            var lessonProvider = new LessonUserInterfaceProvider(XmlRelativePath, hostingEnvironment);
            Steps = lessonProvider.LessonSteps;
        }
    }
}