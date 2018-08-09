using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml.Experiments
{
    [TestClass]
    public class XPathExperiments
    {
        public const string XmlSample = @"
<book>
    <author>John Doe</author>
    <price>12€</price>
</book>";

        [TestMethod]
        public void BasicTest()
        {
            var settings = new XmlReaderSettings { NameTable = new NameTable() };
            var manager = new XmlNamespaceManager(settings.NameTable);
            var context = new XmlParserContext(settings.NameTable, manager, "", XmlSpace.Default);
            using (var stringReader = new StringReader(XmlSample))
            using (var xmlReader = XmlReader.Create(stringReader, settings, context))
            {
                var document = new XPathDocument(xmlReader);
                var navigator = document.CreateNavigator();
                var query1 = XPathExpression.Compile("/book");
                var query2 = XPathExpression.Compile("/book");
                query1.SetContext(manager);
                query2.SetContext(manager);
                var result = (XPathNodeIterator)navigator.Evaluate(query1);
                result.MoveNext();
                var relquery = XPathExpression.Compile("dot:Button");
                relquery.SetContext(manager);
                var relresult = result.Current.Evaluate(relquery);
            }
        }
    }
}
