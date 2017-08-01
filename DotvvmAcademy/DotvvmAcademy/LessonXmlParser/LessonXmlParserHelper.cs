using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace DotvvmAcademy.LessonXmlParser
{
    public static class LessonXmlParserHelper
    {
        /// <summary>
        /// Get attribute of element by attributeName.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="attributeName"></param>
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

        /// <summary>
        /// Get string value of element.
        /// </summary>
        /// <param name="element"></param>
        public static string GetElementStringValue(this XElement element)
        {
            var elementValue = element.Value;
            if (elementValue != null)
            {
                elementValue = elementValue.Trim();
                elementValue = elementValue.TrimEnd();
                return elementValue;
            }
            var message = string.Format(LessonXmlParserErrorMessages.ElementHasNoValue, element);
            throw new InvalidDataException(message);
        }

        /// <summary>
        /// Get collection of child with name childItemsElementName from element with name childElementName which is in parentElement.
        /// </summary>
        /// <param name="parentElement"></param>
        /// <param name="childElementName"></param>
        /// <param name="childItemsElementName"></param>
        /// <returns> Collection of child from childElementName by childItemsElementName. </returns>
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

        /// <summary>
        /// Get string value of child parentElement by childElementName.
        /// </summary>
        /// <param name="parentElement"></param>
        /// <param name="childElementName"></param>
        public static string GetChildElementStringValue(this XElement parentElement, string childElementName)
        {
            return parentElement.GetChildElement(childElementName).GetElementStringValue();
        }

        /// <summary>
        /// Check if parentElement have element by childElementName.
        /// </summary>
        /// <param name="parentElement"></param>
        /// <param name="childElementName"></param>
        public static bool HaveChildElement(this XElement parentElement, string childElementName)
        {
            var element = parentElement.Element(childElementName);
            return element != null;
        }

        /// <summary>
        /// Get child element from parentElement by childElementName.
        /// </summary>
        /// <param name="parentElement"></param>
        /// <param name="childElementName"></param>
        private static XElement GetChildElement(this XElement parentElement, string childElementName)
        {
            var element = parentElement.Element(childElementName);
            if (element != null)
            {
                return element;
            }
            var message = string.Format(LessonXmlParserErrorMessages.ElementChildNotFound, parentElement.Name,
                childElementName);
            throw new InvalidDataException(message);
        }
    }
}