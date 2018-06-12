using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotvvmAcademy.Validation.CSharp.Tests
{
    [TestClass]
    public class MetadataNameParserTests
    {
        [TestMethod]
        public void BasicNameTest()
        {
            var reflectionFormatter = new ReflectionMetadataNameFormatter();
            var userFormatter = new UserFriendlyMetadataNameFormatter();
            var formatter = new MetadataNameFormatter();
            var factory = new MetadataNameFactory(formatter, reflectionFormatter, userFormatter);
            var parser = new MetadataNameParser(factory);
            var name = parser.Parse("CourseFormat.Test");
        }
    }
}