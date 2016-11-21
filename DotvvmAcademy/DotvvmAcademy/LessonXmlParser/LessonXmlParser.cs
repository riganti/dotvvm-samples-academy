using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using DotvvmAcademy.Steps.StepBuilder;
using DotvvmAcademy.Steps.StepsBases.Interfaces;

namespace DotvvmAcademy.LessonXmlParser
{
    public class LessonXmlParser
    {
        public List<IStep> ParseXmlToSteps(string lessonXmlRelativePath)
        {
            var xmlText = GetXmlTextRelativePath(lessonXmlRelativePath);
            var rootElement = CreateXElementFromText(xmlText);
            var stepChildCollection = rootElement.GetChildCollection(LessonXmlElements.StepsElement,
                LessonXmlElements.StepElement);
            return CreateSteps(stepChildCollection);
        }


        private static List<IStep> CreateSteps(IEnumerable<XElement> stepChildCollection)
        {
            var result = new List<IStep>();
            var iterator = 1;
            foreach (var stepElement in stepChildCollection)
            {
                var stepBuilder = new StepBuilder();
                var step = stepBuilder.CreateStep(stepElement, iterator);
                result.Add(step);
                iterator++;
            }
            return result;
        }

        private static string GetXmlTextRelativePath(string lessonXmlRelativePath)
        {
            try
            {
                var absolutePath = Path.Combine(Directory.GetCurrentDirectory(), lessonXmlRelativePath);
                var result = File.ReadAllText(absolutePath);
                return result;
            }
            catch (Exception ex)
            {
                //todo UI Exception
                throw;
            }
        }

        private static XElement CreateXElementFromText(string xmlText)
        {
            try
            {
                return XElement.Parse(xmlText);
            }
            catch (Exception)
            {
                //todo UI Exception
                throw;
            }
        }
    }
}