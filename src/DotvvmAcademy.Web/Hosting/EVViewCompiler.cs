using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.Validation;
using DotVVM.Framework.Configuration;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Immutable;
using System.Linq;

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

        public MetadataReference AdditionalReference { get; set; }

        //protected override CSharpCompilation AddToCompilation(
        //    CSharpCompilation compilation, 
        //    DefaultViewCompilerCodeEmitter emitter, 
        //    string fileName, 
        //    string namespaceName, 
        //    string className)
        //{
        //    var tree = emitter.BuildTree(namespaceName, className, fileName);
        //    return compilation
        //        .AddSyntaxTrees(tree)
        //        .AddReferences(AdditionalReference.WithAliases(ImmutableArray.Create("global")))
        //        .AddReferences(emitter.UsedAssemblies
        //            .Where(a => !string.IsNullOrEmpty(a.Key.Location))
        //            .Select(a => 
        //            {
        //                return CompiledAssemblyCache.Instance.GetAssemblyMetadata(a.Key).WithAliases(ImmutableArray.Create(a.Value, "global"));
        //            }));
        //}
    }
}