using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace DotvvmAcademy.LessonXmlParser
{
    public static class LessonXmlParserHelper
    {
        public static XAttribute GetAttribute(this XElement element, string attributeName)
        {
            var attribute = element.Attribute(attributeName);
            if (attribute != null)
            {
                return attribute;
            }
            throw new InvalidDataException(
                $"Element: \"{element.Name}\" has no attribute: \"{attributeName}\"");
        }


        


        public static IEnumerable<XElement> GetChildCollection(this XElement parentElement, string childElementName)
        {
            var result = parentElement.Elements(childElementName);
            if (result.Any())
            {
                return result;
            }
            throw new InvalidDataException(
                $"XML file doesn`t contains childs elements: \"{childElementName}\" in parent element: \"{parentElement.Name}\"");
        }

        public static string GetChildElementStringValue(this XElement parentElement, string childElementName)
        {
            if (parentElement.HaveChildElement(childElementName))
            {
                return parentElement.Element(childElementName).GetElementStringValue();
            }
            throw new InvalidDataException(
                $"XML file doesn`t contains element: \"{childElementName}\" in parent element: \"{parentElement.Name}\"");
        }

        public static string GetElementStringValue(this XElement element)
        {
            var result = element.Value;
            if (result != null)
            {
                result = result.Trim();
                result = result.TrimEnd();
                return result;
            }
            throw new InvalidDataException(
                $"Element: \"{element}\" has no value");
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

        public static bool HaveChildElement(this XElement parentElement, string childName)
        {
            var result = parentElement.Element(childName);
            return result != null;
        }

        public static IEnumerable<XElement> GetChildCollection(this XElement rootElement, string childElementName,
            string childCollectionElementName)
        {
            return rootElement.GetChildElement(childElementName).GetChildCollection(childCollectionElementName);
        }
    }
}