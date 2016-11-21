using System.Collections.Generic;
using DotvvmAcademy.Steps.StepsBases.Interfaces;

namespace DotvvmAcademy.Services
{
    public class LessonUserInterfaceProvider
    {
        public LessonUserInterfaceProvider(string lessonXmlRelativePath)
        {
            var parser = new LessonXmlParser.LessonXmlParser();
            LessonSteps = parser.ParseXmlToSteps(lessonXmlRelativePath);
        }

        public List<IStep> LessonSteps { get; set; }
    }
}