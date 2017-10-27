using DotvvmAcademy.Validation.CSharp.Abstractions;

namespace DotvvmAcademy.Validation.Tests
{
    [ValidationClass]
    public class CSharpSampleValidationClass
    {
        [ValidationMethod]
        public void SampleValidationMethod(ICSharpDocument document)
        {
            var testClass = document.GlobalNamespace().Class("Test");
            var testProperty = testClass.Property("TestProperty");
            testProperty.Type(typeof(int));
            var testMethod = testClass.Method("TestMethod");
            testMethod.ReturnType(typeof(string));
        }
    }
}