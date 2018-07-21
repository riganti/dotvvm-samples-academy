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
            var factory = new MetadataNameFactory();
            var parser = new MetadataNameParser(factory);
            var name = parser.Parse("CourseFormat.Test");
        }
    }
}