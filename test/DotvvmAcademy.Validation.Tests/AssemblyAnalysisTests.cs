using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.CSharp.AssemblyAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.Tests
{

    [TestClass]
    public class AssemblyAnalysisTests
    {
        [TestMethod]
        public async Task AssemblyRewriteTest()
        {
            var request = CSharpValidationUtilities.CreateRequest(CSharpSampleSources.NeverEndingSample);
            var validator = CSharpValidationUtilities.CreateValidator();
            request.ValidationExtent = CSharpValidationExtent.AssemblyRewrite;
            var response = await validator.Validate(request);
            var type = response.EmittedAssembly.GetType("Test");
            var test = Activator.CreateInstance(type);
            var testMethod = type.GetMethod("TestMethod");
            try
            {
                testMethod.Invoke(test, new object[0]);
            }
            catch(AssemblySafeguardException)
            {
            }
        }
    }
}
