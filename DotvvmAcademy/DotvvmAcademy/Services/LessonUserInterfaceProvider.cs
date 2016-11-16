using System.Collections.Generic;
using DotvvmAcademy.Lessons;
using DotvvmAcademy.Steps;

namespace DotvvmAcademy.Services
{
    public class LessonUserInterfaceProvider
    {
        public LessonUserInterfaceProvider(LessonBase lessonBase, string lessonXmlRelativePath)
        {
            var parser = new LessonXmlParser();
            LessonSteps = parser.ParseXmlToSteps(lessonXmlRelativePath, lessonBase);
        }

        public IEnumerable<StepBase> LessonSteps { get; set; }
    }
}