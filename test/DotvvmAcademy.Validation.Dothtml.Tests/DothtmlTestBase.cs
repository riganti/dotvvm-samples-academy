using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Tokenizer;
using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            parser.Parse(tokenizer.Tokens);
        }

        protected virtual void RegisterServices(IServiceCollection container)
        {
            container.AddScoped<ValidationControlResolver>();
            container.AddScoped<ValidationTreeBuilder>();
            container.AddScoped<ValidationTypeDescriptorFactory>();
            container.AddScoped<ValidationControlTypeFactory>();
            container.AddScoped<ValidationTypeDescriptorFactory>();
            container.AddScoped<ValidationTreeResolver>();
        }
    }
}