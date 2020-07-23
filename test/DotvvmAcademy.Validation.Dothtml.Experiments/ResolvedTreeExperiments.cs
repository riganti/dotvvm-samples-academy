using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Tokenizer;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Security;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DotvvmAcademy.Validation.Dothtml.Experiments
{
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

        [Fact(Skip = "DotvvmConfiguration fails.")]
        public void DothtmlCompiler_CompileSample_DoesNotThrow()
        {
            var tokenizer = new DothtmlTokenizer();
            var parser = new DothtmlParser();
            var config = DotvvmConfiguration.CreateDefault(c => c.AddSingleton<IViewModelProtector, FakeProtector>());
            var resolver = config.ServiceProvider.GetRequiredService<IControlTreeResolver>();
            tokenizer.Tokenize(Sample);
            var rootNode = parser.Parse(tokenizer.Tokens);
            var root = resolver.ResolveTree(rootNode, "Test.dothtml");
        }

        [Fact(Skip = "DotvvmConfiguration fails.")]
        public void DothtmlCompiler_WithAttributes_ResolvesProperty()
        {
            var tokenizer = new DothtmlTokenizer();
            var parser = new DothtmlParser();
            var config = DotvvmConfiguration.CreateDefault(c => c.AddSingleton<IViewModelProtector, FakeProtector>());
            var resolver = config.ServiceProvider.GetRequiredService<IControlTreeResolver>();
            tokenizer.Tokenize("<meta charset=\"utf-8\" \\>");
            var rootNode = parser.Parse(tokenizer.Tokens);
            var root = resolver.ResolveTree(rootNode, "Test.dothtml");
        }
    }
}
