using DotvvmAcademy.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace DotvvmAcademy.CommonMark
{
    public class SampleControlParser : IControlParser<SampleControl>
    {
        public const string ElementName = "sample";
        public const string IncorrectAttributeName = "Incorrect";
        public const string CorrectAttributeName = "Correct";
        public const string ValidatorAttributeName = "Validator";

        public bool CanParse(XElement element)
        {
            return element.Name == ElementName;
        }

        public SampleControl Parse(XElement element)
        {
            var sampleControl = new SampleControl()
            {
                IncorrectPath = GetRequiredAttributeValue(element, IncorrectAttributeName),
                CorrectPath = GetRequiredAttributeValue(element, CorrectAttributeName),
                ValidatorName = GetRequiredAttributeValue(element, ValidatorAttributeName)
            };
            return sampleControl;
        }

        private string GetRequiredAttributeValue(XElement element, string attributeName)
        {
            var attribute = element.Attribute(XName.Get(IncorrectAttributeName));
            if (attribute == null)
            {
                throw new ControlParserException($"The '{ElementName}' element is missing the '{attributeName}' attribute.");
            }
            return attribute.Value;
        }
    }
}
