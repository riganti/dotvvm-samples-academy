using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using DotvvmAcademy.Lessons;
using DotvvmAcademy.Steps;

namespace DotvvmAcademy.Helpers
{
    public static class XmlParserHelper
    {
        public static XElement CreateXElementFromText(string xmlText)
        {
            try
            {
                return XElement.Parse(xmlText);
            }
            catch (Exception)
            {
                //todo
                throw;
            }
        }


        public static bool IsStepType(this XElement stepElement, string type)
        {
            //todo Type / move to some config file?  
            var result = stepElement.Attribute("Type")?.Value;
            if (result != null)
            {
                return result == type;
            }
            throw new InvalidDataException(
                $"XML file doesn`t contains atribute: \"{type}\" in element: \"{stepElement.Name}\"");
        }

        public static string GetXmlTextRelativePath(string lessonXmlRelativePath)
        {
            try
            {
                var absolutePath = Path.Combine(Directory.GetCurrentDirectory(), lessonXmlRelativePath);
                var result = File.ReadAllText(absolutePath);
                return result;
            }
            catch (Exception ex)
            {
                //todo
                throw;
            }
        }


        public static string GetXmlTextAbsolutePath(string absolutePath)
        {
            try
            {
                var result = File.ReadAllText(absolutePath);
                return result;
            }
            catch (Exception ex)
            {
                //todo
                throw;
            }
        }

        public static IEnumerable<XElement> GetChildCollection(this XElement parentElement, string childName)
        {
            var result = parentElement.Elements(childName);
            if (result.Any())
            {
                return result;
            }
            throw new InvalidDataException(
                $"XML file doesn`t contains childs elements: \"{childName}\" in parent element: \"{parentElement.Name}\"");
        }

        public static XElement GetChildElement(this XElement parentElement, string childName)
        {
            var result = parentElement.Element(childName);
            if (result != null)
            {
                return result;
            }
            throw new InvalidDataException(
                $"XML file doesn`t contains child element: \"{childName}\" in parent element: \"{parentElement.Name}\"");
        }


        public static string GetElementValueString(this XElement parentElement, string elementName)
        {
            var result = parentElement.Element(elementName)?.Value;
            if (result != null)
            {
                return result;
            }
            throw new InvalidDataException(
                $"XML file doesn`t contains element: \"{elementName}\" in parent element: \"{parentElement.Name}\"");
        }

        public static int GetElementValueInt(this XElement parentElement, string elementName)
        {
            var elementValue = GetElementValueString(parentElement, elementName);
            var res = elementValue.ToNullableInt();
            if (res == null)
            {
                throw new InvalidCastException(
                    $"I can`t cast element: \"{elementName}\" with value \"{elementValue}\" to int. Parent element: \"{parentElement.Name}\" ");
            }
            return res.Value;
        }

        //private static StepBase 


        private static InfoStep CreateInfoStep(this XElement step, LessonBase currentLessonBase, int iterator)
        {
            var result = new InfoStep(currentLessonBase);
            result.FillStepBasicData(step, iterator);
            return result;
        }

        private static void FillStepBasicData(this StepBase stepBase, XElement step, int iterator)
        {
            stepBase.StepIndex = iterator;
            stepBase.Description = step.GetElementValueString("Description");
            stepBase.Title = step.GetElementValueString("Title");
        }

        private static void FillStepCodeData(this CodeBaseStep stepCodeBase, XElement step, int iterator)
        {
            stepCodeBase.FillStepBasicData(step, iterator);
            stepCodeBase.StartupCode = step.GetElementValueString("StartupCode");
            stepCodeBase.FinalCode = step.GetElementValueString("FinalCode");
            stepCodeBase.ShadowBoxDescription = step.GetElementValueString("ShadowBoxDescription");
        }



        private static DothtmlStep CreateDothtmlStep(this XElement step, LessonBase currentLessonBase, int iterator)
        {
            //todo validation
            var result = new DothtmlStep(currentLessonBase);
            result.FillStepCodeData(step, iterator);
            return result;
        }

        private static CodeStep CreateCodeStep(this XElement step, LessonBase currentLessonBase, int iterator)
        {
            //todo validation
            var result = new CodeStep(currentLessonBase);
            result.FillStepCodeData(step, iterator);
            return result;
        }


        public static StepBase CreateStep(this XElement stepElement, LessonBase currentLessonBase, int iterator)
        {
            if (stepElement.IsStepType("Code"))
            {
                return stepElement.CreateCodeStep(currentLessonBase, iterator);
            }
            if (stepElement.IsStepType("Dothtml"))
            {
                return stepElement.CreateDothtmlStep(currentLessonBase, iterator);
            }

            if (stepElement.IsStepType("Info"))
            {
                return stepElement.CreateInfoStep(currentLessonBase, iterator);
            }
            throw new InvalidDataException($"Step type {stepElement.Name} ins`t supported");
        }
    }
}