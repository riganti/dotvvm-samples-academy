using DotvvmAcademy.BL.DTO;
using DotvvmAcademy.BL.DTO.Components;
using System.Xml.Linq;

namespace DotvvmAcademy.BL.CommonMark
{
    public class SampleComponentParser : IComponentParser<SampleComponent>
    {
        public const string CorrectAttributeName = "Correct";

        public const string ElementName = "sample";

        public const string IncorrectAttributeName = "Incorrect";

        public const string ValidatorAttributeName = "Validator";

        public bool CanParse(XElement element)
        {
            return element.Name == ElementName;
        }

        public SampleComponent Parse(XElement element, int lessonIndex, string language, int stepIndex)
        {
            var sample = new SampleComponent(lessonIndex, language, stepIndex)
            {
                CorrectPath = GetRequiredAttributeValue(element, CorrectAttributeName),
                IncorrectPath = GetRequiredAttributeValue(element, IncorrectAttributeName),
                Validator = GetRequiredAttributeValue(element, ValidatorAttributeName)
            };
            return sample;
        }

        private string GetRequiredAttributeValue(XElement element, string attributeName)
        {
            var attribute = element.Attribute(XName.Get(attributeName));
            if (attribute == null)
            {
                throw new ComponentParserException($"The '{ElementName}' element is missing the '{attributeName}' attribute.");
            }
            return attribute.Value;
        }
    }
}