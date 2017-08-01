using DotvvmAcademy.Steps.StepsBases.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;

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