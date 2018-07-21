using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Tokenizer;
using DotVVM.Framework.Configuration;
using DotvvmAcademy.Meta;
using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.XPath;

namespace DotvvmAcademy.Validation.Dothtml.Tests
{
    [TestClass]
    public class XPathResolvedTreeNavigatorTests
    {
        public const string BasicView = @"
@viewModel ValidationTreeSample.BasicViewModel, ValidationTreeSample
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

        [TestMethod]
        public void CustomXPathTest()
        {
            var services = GetMinimalServices();
            var controlResolver = services.GetRequiredService<ValidationControlResolver>();
            controlResolver.RegisterNamespace("dot", "DotVVM.Framework.Controls", "DotVVM.Framework");
            var tokenizer = services.GetRequiredService<DothtmlTokenizer>();
            var parser = services.GetRequiredService<DothtmlParser>();
            var resolver = services.GetRequiredService<ValidationTreeResolver>();
            var xpathVisitor = services.GetRequiredService<XPathTreeVisitor>();
            tokenizer.Tokenize(BasicView);
            var rootNode = parser.Parse(tokenizer.Tokens);
            var tree = (ValidationTreeRoot)resolver.ResolveTree(rootNode, "BasicView.dothtml");
            var xpathRoot = xpathVisitor.Visit(tree);
            var navigator = new XPathDothtmlNavigator(xpathRoot);
            var query = XPathExpression.Compile("//TextBox/@Text");
            var result = navigator.Evaluate(query);
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

        private CSharpCompilation CompileViewModel()
        {
            var tree = CSharpSyntaxTree.ParseText(BasicViewModel);
            var compilation = CSharpCompilation.Create(
                assemblyName: "ValidationTreeSample",
                syntaxTrees: new[] { tree },
                references: new[]
                {
                    MetadataReferencer.FromName("netstandard"),
                    MetadataReferencer.FromName("System.Private.CoreLib"),
                    MetadataReferencer.FromName("System.Runtime"),
                    MetadataReferencer.FromName("System.Collections"),
                    MetadataReferencer.FromName("System.Reflection"),
                    MetadataReferencer.FromName("DotVVM.Framework"),
                    MetadataReferencer.FromName("DotVVM.Core")
                },
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            return compilation;
        }

        private IServiceProvider GetMinimalServices()
        {
            var c = new ServiceCollection();
            c.AddScoped(p => CompileViewModel());
            c.AddScoped<AttributeExtractor>();
            c.AddScoped<ValidationTypeDescriptorFactory>();
            c.AddScoped<ValidationControlTypeFactory>();
            c.AddScoped<ValidationControlMetadataFactory>();
            c.AddScoped<ValidationPropertyDescriptorFactory>();
            c.AddScoped<ValidationControlResolver>();
            c.AddScoped<ValidationTreeResolver>();
            c.AddScoped<ValidationTreeBuilder>();
            c.AddScoped<DothtmlTokenizer>();
            c.AddScoped<DothtmlParser>();
            c.AddScoped<XPathTreeVisitor>();
            c.AddScoped<ValidationReporter>();
            return c.BuildServiceProvider();
        }

        private MetadataReference GetReference(string name)
        {
            return MetadataReference.CreateFromFile(Assembly.Load(name).Location);
        }
    }
}