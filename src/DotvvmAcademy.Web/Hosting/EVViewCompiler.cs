using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.Validation;
using DotVVM.Framework.Configuration;
using Microsoft.Extensions.Options;
using System;

namespace DotvvmAcademy.Web.Hosting
{
    public class EVViewCompiler : DefaultViewCompiler
    {
        public EVViewCompiler(
            IOptions<ViewCompilerConfiguration> config,
            EVControlTreeResolver controlTreeResolver,
            IBindingCompiler bindingCompiler,
            Func<ControlUsageValidationVisitor> controlValidatorFactory,
            DotvvmMarkupConfiguration markupConfiguration)
            : base(config, controlTreeResolver, bindingCompiler, controlValidatorFactory, markupConfiguration)
        {
        }
    }
}