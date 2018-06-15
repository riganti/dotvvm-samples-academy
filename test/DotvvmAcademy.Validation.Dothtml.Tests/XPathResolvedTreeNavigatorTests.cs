using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Tokenizer;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Security;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml.Tests
{
    [TestClass]
    public class XPathResolvedTreeNavigatorTests
    {
        public const string BasicSource = @"
<div viewModel=""Test"">
    <div>
        <dot:Button Click=""{command: OnClick()}"" />
    </div>
    <dot:TextBox Text=""{value: Text1}"" />
    <dot:TextBox Text=""{value: Text2}"" />
    <dot:TextBox Text=""{value: Text3}"" />
</div>";

        [TestMethod]
        public void VanillaXPathTest()
        {
            var settings = new XmlReaderSettings { NameTable = new NameTable() };
            var manager = new XmlNamespaceManager(settings.NameTable);
            manager.AddNamespace("dot", "VanillaDotvvmControls");
            var context = new XmlParserContext(settings.NameTable, manager, "", XmlSpace.Default);
            using(var stringReader = new StringReader(BasicSource))
            using(var xmlReader = XmlReader.Create(stringReader, settings, context))
            {
                var document = new XPathDocument(xmlReader);
                var navigator = document.CreateNavigator();
                var query = XPathExpression.Compile("/div/div");
                query.SetContext(manager);
                var result = (XPathNodeIterator)navigator.Evaluate(query);
                result.MoveNext();
                var relquery = XPathExpression.Compile("dot:Button");
                relquery.SetContext(manager);
                var relresult = result.Current.Evaluate(relquery);
            }
        }

        [TestMethod]
        public void CustomXPathTest()
        {
            var tokenizer = new DothtmlTokenizer();
            var parser = new DothtmlParser();
            var configuration = DotvvmConfiguration.CreateDefault(c => c.AddSingleton<IViewModelProtector, FakeProtector>());
            var resolver = configuration.ServiceProvider.GetRequiredService<IControlTreeResolver>();
            tokenizer.Tokenize(BasicSource);
            var tree = (ResolvedTreeRoot)resolver.ResolveTree(parser.Parse(tokenizer.Tokens), "BasicSource.dothtml");
            //var navigator = new XPathDothtmlNavigator(tree);
        }

    }
}
