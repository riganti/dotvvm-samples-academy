using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Tokenizer;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Security;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotvvmAcademy.Validation.Dothtml.Experiments
{
    [TestClass]
    public class ResolvedTreeExperiments
    {

        public const string Sample = @"
@viewModel System.Object
@service System.IServiceProvider
@import L=System.Linq

<!doctype html>
<html>
    <body>
        <dot:Literal Text=""SAMPLE"" />
    </body>
</html>";

        [TestMethod]
        public void BasicCompilerExperiment()
        {
            var tokenizer = new DothtmlTokenizer();
            var parser = new DothtmlParser();
            var config = DotvvmConfiguration.CreateDefault(c => c.AddSingleton<IViewModelProtector, FakeProtector>());
            var resolver = config.ServiceProvider.GetRequiredService<IControlTreeResolver>();
            tokenizer.Tokenize(Sample);
            var rootNode = parser.Parse(tokenizer.Tokens);
            var root = resolver.ResolveTree(rootNode, "Test.dothtml");
        }
    }
}
