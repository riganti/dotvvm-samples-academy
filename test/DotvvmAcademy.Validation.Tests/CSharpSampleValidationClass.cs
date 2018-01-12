using DotvvmAcademy.Validation.CSharp.Unit.Abstractions;

namespace DotvvmAcademy.Validation.Tests
{
    [ValidationClass]
    public class CSharpSampleValidationClass
    {
        [ValidationMethod]
        public void SampleValidationMethod(ICSharpDocument document)
        {
            var testClass = document.GetGlobalNamespace().GetClass("Test");
            var testProperty = testClass.GetAutoProperty("TestProperty");
            testProperty.Type = typeof(int);
            var testMethod = testClass.GetMethod("TestMethod");
            testMethod.ReturnType = typeof(string);
        }
    }
}