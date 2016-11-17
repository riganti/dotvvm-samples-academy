using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

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
            //todo to resources
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
                result = result.Trim();
                result = result.TrimEnd();
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
    }
}