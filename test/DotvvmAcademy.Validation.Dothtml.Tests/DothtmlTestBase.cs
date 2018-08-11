using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Tokenizer;
using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DotvvmAcademy.Validation.Dothtml.Tests
{
    public abstract class DothtmlTestBase
    {
        public DothtmlTestBase()
        {
            Provider = BuildServiceProvider();
        }

        protected IServiceProvider Provider { get; }

        protected virtual IServiceProvider BuildServiceProvider()
        {
            var container = new ServiceCollection();
            RegisterServices(container);
            return container.BuildServiceProvider();
        }

        protected virtual ValidationTreeRoot GetValidationTree(string dothtml, string viewModel = null)
        {
            var tokenizer = new DothtmlTokenizer();
            tokenizer.Tokenize(dothtml);
            var parser = new DothtmlParser();
            var root = parser.Parse(tokenizer.Tokens);
            using (var scope = Provider.CreateScope())
            {
                var resolver = Provider.GetRequiredService<ValidationTreeResolver>();
                return (ValidationTreeRoot)resolver.ResolveTree(root, "test.dothtml");
            }
        }

        protected virtual void RegisterServices(IServiceCollection container)
        {
            container.AddMetaScopeFriendly();
            container.AddScoped<ValidationControlResolver>();
            container.AddScoped<ValidationTreeBuilder>();
            container.AddScoped<ValidationTypeDescriptorFactory>();
            container.AddScoped<ValidationControlTypeFactory>();
            container.AddScoped<ValidationTypeDescriptorFactory>();
            container.AddScoped<ValidationTreeResolver>();
        }
    }
}