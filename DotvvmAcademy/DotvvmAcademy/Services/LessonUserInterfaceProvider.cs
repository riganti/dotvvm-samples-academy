using System.Collections.Generic;
using DotvvmAcademy.Steps.StepsBases.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace DotvvmAcademy.Services
{
    public class LessonUserInterfaceProvider
    {
        public LessonUserInterfaceProvider(string lessonXmlRelativePath, IHostingEnvironment hostingEnvironment)
        {
            var parser = new LessonXmlParser.LessonXmlParser(hostingEnvironment);
            LessonSteps = parser.ParseXmlToSteps(lessonXmlRelativePath);
        }

        public List<IStep> LessonSteps { get; set; }
    }
}