using System.Collections.Generic;
using DotvvmAcademy.Lessons;
using DotvvmAcademy.Steps.StepsBases;

namespace DotvvmAcademy.Services
{
    public class LessonUserInterfaceProvider
    {
        public LessonUserInterfaceProvider(string lessonXmlRelativePath)
        {
            var parser = new LessonXmlParser();
            LessonSteps = parser.ParseXmlToSteps(lessonXmlRelativePath);
        }

        public IEnumerable<StepBase> LessonSteps { get; set; }
    }
}