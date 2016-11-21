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
            throw new InvalidDataException(string.Format(LessonXmlParserErrorMessages.ElementAttributeNotFound,
                element.Name, attributeName));
        }


        public static string GetChildElementStringValue(this XElement parentElement, string childElementName)
        {
            return parentElement.GetChildElement(childElementName).GetElementStringValue();
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
            var message = string.Format(LessonXmlParserErrorMessages.ElementHasNoValue, element);
            throw new InvalidDataException(message);
        }


        private static XElement GetChildElement(this XElement parentElement, string childElementName)
        {
            var result = parentElement.Element(childElementName);
            if (result != null)
            {
                return result;
            }
            var message = string.Format(LessonXmlParserErrorMessages.ElementChildNotFound, parentElement.Name,
                childElementName);
            throw new InvalidDataException(message);
        }

        public static bool HaveChildElement(this XElement parentElement, string childElementName)
        {
            var result = parentElement.Element(childElementName);
            return result != null;
        }

        public static IEnumerable<XElement> GetChildCollection(this XElement parentElement, string childElementName,
            string childItemsElementName)
        {
            var child = parentElement.GetChildElement(childElementName);
            var chilItems = child.Elements(childItemsElementName);
            var childCollection = chilItems as IList<XElement> ?? chilItems.ToList();
            if (childCollection.Any())
            {
                return childCollection;
            }
            var message = string.Format(LessonXmlParserErrorMessages.ElementChildrenNotFound, parentElement.Name,
                childElementName);
            throw new InvalidDataException(message);
        }
    }
}