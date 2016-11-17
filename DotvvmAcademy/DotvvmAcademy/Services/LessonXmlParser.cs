using System.Collections.Generic;
using System.Xml.Linq;
using DotvvmAcademy.Helpers;
using DotvvmAcademy.Lessons;
using DotvvmAcademy.Steps.StepsBases;

namespace DotvvmAcademy.Services
{
    public class LessonXmlParser
    {
        public List<StepBase> ParseXmlToSteps(string lessonXmlRelativePath, LessonBase currentLessonBase)
        {
            var xmlText = XmlParserHelper.GetXmlTextRelativePath(lessonXmlRelativePath);
            var rootElement = XmlParserHelper.CreateXElementFromText(xmlText);
            var stepChildCollection = rootElement.GetChildElement("Steps").GetChildCollection("Step");
            return CreateSteps(currentLessonBase, stepChildCollection);
        }


        private static List<StepBase> CreateSteps(LessonBase currentLessonBase,
            IEnumerable<XElement> stepChildCollection)
        {
            var result = new List<StepBase>();
            var iterator = 1;
            foreach (var stepElement in stepChildCollection)
            {
                var step = stepElement.CreateStep(currentLessonBase, iterator);
                result.Add(step);
                iterator++;
            }
            return result;
        }
    }
}