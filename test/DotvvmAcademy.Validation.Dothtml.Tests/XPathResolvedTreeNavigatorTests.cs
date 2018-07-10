using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Tokenizer;
using DotVVM.Framework.Security;
using DotVVM.Framework.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using DotVVM.Framework.Configuration;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System.Reflection;

namespace DotvvmAcademy.Validation.Dothtml.Tests
{
    [TestClass]
    public class XPathResolvedTreeNavigatorTests
    {
        public const string BasicViewModel = @"
using System;

namespace ValidationTreeSample
{
    public class BasicViewModel
    {
        public string Text1 { get; set; } = ""I'm not"";

        public string Text2 { get; set; } = ""throwing away"";

        public string Text3 { get; set; } = ""my shot!"";

        public string Result { get; set; } = ""-----"";

        public void OnClick()
        {
            Result = $""{Text1} {Text2} {Text3}"";
        }
    }
}";

        public const string BasicView = @"
@viewModel DotvvmAcademy.Validation.Dothtml.Tests.BasicViewModel
<html>
    <body>
        <div>
            <dot:Button Click=""{command: OnClick()}"" />
        </div>
        <dot:TextBox Text=""{value: Text1}"" />
        <dot:TextBox Text=""{value: Text2}"" />
        <dot:TextBox Text=""{value: Text3}"" />
        {{value: Result}}
    </body>
</html>";

        [TestMethod]
        public void CustomXPathTest()
        {
            var services = GetMinimalServices();
            var tokenizer = services.GetRequiredService<DothtmlTokenizer>();
            var parser = services.GetRequiredService<DothtmlParser>();
            var resolver = services.GetRequiredService<IControlTreeResolver>();
            var aggregator = services.GetRequiredService<ErrorAggregatingVisitor>();
            var xpathVisitor = services.GetRequiredService<XPathResolvedTreeVisitor>();
            tokenizer.Tokenize(BasicView);
            var rootNode = parser.Parse(tokenizer.Tokens);
            var tree = (ResolvedTreeRoot)resolver.ResolveTree(rootNode, "BasicView.dothtml");
            tree.Accept(aggregator);
            tree.Accept(xpathVisitor);
            var navigator = new XPathDothtmlNavigator(xpathVisitor.Root);
            var query = XPathExpression.Compile("/html//Repeater/@ItemTemplate/span");
            var result = navigator.Evaluate(query);
        }

        public IServiceProvider GetMinimalServices()
        {
            var c = new ServiceCollection();
            c.AddScoped<CSharpCompilation>(p => CompileViewModel());
            c.AddScoped<ValidationTypeDescriptorFactory>();
            c.AddScoped<DotvvmMarkupConfiguration>(p => new DotvvmMarkupConfiguration());
            c.AddScoped<IControlResolver, ValidationControlResolver>();
            c.AddScoped<IControlTreeResolver, ValidationTreeResolver>();
            c.AddScoped<IControlBuilderFactory, FakeControlBuilderFactory>();
            c.AddScoped<IAbstractTreeBuilder, ValidationTreeBuilder>();
            c.AddScoped<DothtmlTokenizer>();
            c.AddScoped<DothtmlParser>();
            c.AddScoped<ErrorAggregatingVisitor>();
            c.AddScoped<XPathResolvedTreeVisitor>();
            return c.BuildServiceProvider();
        }

        public CSharpCompilation CompileViewModel()
        {
            var tree = CSharpSyntaxTree.ParseText(BasicViewModel);
            var compilation = CSharpCompilation.Create(
                assemblyName: "ValidationTreeSample",
                syntaxTrees: new [] {tree},
                references: new [] {GetReference("System.Private.CoreLib")},
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            return compilation;
        }

        public MetadataReference GetReference(string name)
        {
            return MetadataReference.CreateFromFile(Assembly.Load(name).Location);
        }

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
    }
}