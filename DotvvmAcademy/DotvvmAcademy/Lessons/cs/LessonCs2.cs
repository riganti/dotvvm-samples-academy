using DotvvmAcademy.Services;
using Microsoft.AspNetCore.Hosting;

namespace DotvvmAcademy.Lessons
{
    public class LessonCs2 : LessonBase
    {
        public LessonCs2(IHostingEnvironment hostingEnvironment)
        {
            var lesson2RelativePath = @"Lessons\cs\Lesson2.xml";
            var lessonProvider = new LessonUserInterfaceProvider(lesson2RelativePath, hostingEnvironment);
            Steps = lessonProvider.LessonSteps;
        }
    }
}