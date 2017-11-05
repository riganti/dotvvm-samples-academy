using DotvvmAcademy.Validation.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace DotvvmAcademy.Validation.Tests
{
    [TestClass]
    public class CSharpTests
    {
        [TestMethod]
        public void BasicTest()
        {
            var builder = new DefaultCSharpValidatorBuilder();
            builder.AddValidationAssembly(typeof(CSharpTests).Assembly);
            var validator = builder.Build();
            var requestFactory = new DefaultCSharpValidationRequestFactory();
            var request = requestFactory.CreateRequest(CSharpSampleSources.Sample, "SampleValidationMethod");
            var response = validator.Validate(request).Result;
            foreach(var diagnostic in response.Diagnostics)
            {
                Debug.WriteLine($"ValidationDiagnostic: '{diagnostic.Id}', '{diagnostic.Message}'.");
            }
        }


    }
}
