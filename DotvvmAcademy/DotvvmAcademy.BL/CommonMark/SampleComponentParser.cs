using DotvvmAcademy.BL.DTO.Components;
using System.Collections.Immutable;
using System.Linq;
using System.Xml.Linq;

namespace DotvvmAcademy.BL.CommonMark
{
    public class SampleComponentParser : IComponentParser<SampleComponent>
    {
        public const string CorrectAttributeName = "Correct";
        public const string DependenciesElementName = "dependencies";
        public const string DependencyElementName = "dependency";
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
            var dependencies = element.Elements(XName.Get(DependenciesElementName)).SingleOrDefault();
            if(dependencies != null)
            {
                var values = dependencies.Elements(XName.Get(DependencyElementName)).Select(e => e.Value);
                sample.Dependencies = ImmutableList.CreateRange(values);
            }
            else
            {
                sample.Dependencies = Enumerable.Empty<string>();
            }
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