using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.Validation;
using DotVVM.Framework.Configuration;
using Microsoft.Extensions.Options;
using System;

namespace DotvvmAcademy.Web.Hosting
{
    public class EmbeddedViewCompiler : DefaultViewCompiler
    {
        public EmbeddedViewCompiler(
            IOptions<ViewCompilerConfiguration> config,
            EmbeddedViewTreeResolver controlTreeResolver,
            IBindingCompiler bindingCompiler,
            Func<ControlUsageValidationVisitor> controlValidatorFactory,
            CompiledAssemblyCache compiledAssemblyCache)
            : base(config, controlTreeResolver, bindingCompiler, controlValidatorFactory, compiledAssemblyCache)
        {
        }
    }
}
