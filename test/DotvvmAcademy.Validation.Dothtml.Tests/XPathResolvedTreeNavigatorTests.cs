using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Tokenizer;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Security;
using DotVVM.Framework.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml.Tests
{
    public class BasicViewModel : DotvvmViewModelBase
    {
        public List<string> Items { get; set; }

        public string Text1 { get; set; }

        public string Text2 { get; set; }

        public string Text3 { get; set; }

        public void OnClick()
        {
        }
    }

    [TestClass]
    public class XPathResolvedTreeNavigatorTests
    {

        public const string BasicView = @"
@viewModel DotvvmAcademy.Validation.Dothtml.Tests.BasicViewModel
<html>
    <body>
        <div>
            <dot:Button Click=""{command: OnClick()}"" />
        </div>
        <dot:TextBox Text=""Test"" />
        <dot:TextBox Text=""{value: Text2}"" />
        <dot:TextBox Text=""{value: Text3}"" />
        <dot:Repeater DataSource=""{value: Items}"">
            <span>bollocks</span>
        </dot:Repeater>
    </body>
</html>";

        [TestMethod]
        public void VanillaXPathTest()
        {
            var settings = new XmlReaderSettings { NameTable = new NameTable() };
            var manager = new XmlNamespaceManager(settings.NameTable);
            manager.AddNamespace("dot", "VanillaDotvvmControls");
            var context = new XmlParserContext(settings.NameTable, manager, "", XmlSpace.Default);
            using (var stringReader = new StringReader(BasicView))
            using (var xmlReader = XmlReader.Create(stringReader, settings, context))
            {
                var document = new XPathDocument(xmlReader);
                var navigator = document.CreateNavigator();
                var query = XPathExpression.Compile("/div/dot:TextBox/@Template/div[1]");
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
            var configuration = DotvvmConfiguration.CreateDefault(c =>
            {
                c.AddSingleton<IViewModelProtector, FakeProtector>();
            });
            var resolver = configuration.ServiceProvider.GetRequiredService<IControlTreeResolver>();
            var visitor = new XPathResolvedTreeVisitor();
            var errorAggregator = new ErrorAggregatingVisitor();
            tokenizer.Tokenize(BasicView);
            var rootNode = parser.Parse(tokenizer.Tokens);
            var tree = (ResolvedTreeRoot)resolver.ResolveTree(rootNode, "BasicView.dothtml");
            tree.Accept(errorAggregator);
            tree.Accept(visitor);
            var navigator = new XPathDothtmlNavigator(visitor.Root);
            var query = XPathExpression.Compile("/html//Repeater/@ItemTemplate/span");
            var result = navigator.Evaluate(query);
        }

    }
}
