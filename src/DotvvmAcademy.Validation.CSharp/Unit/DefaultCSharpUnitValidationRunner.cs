using DotvvmAcademy.Validation.CSharp.DynamicAnalysis;
using DotvvmAcademy.Validation.CSharp.StaticAnalysis;
using DotvvmAcademy.Validation.CSharp.Unit.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public class DefaultCSharpUnitValidationRunner : ICSharpUnitValidationRunner
    {
        private readonly IEnumerable<IMetadataExtractor> extractors;
        private readonly IServiceProvider provider;

        public DefaultCSharpUnitValidationRunner(IServiceProvider provider)
        {
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
            extractors = provider.GetRequiredService<IEnumerable<IMetadataExtractor>>();
        }

        public (CSharpStaticAnalysisContext, CSharpDynamicAnalysisContext) Run(MethodInfo method)
        {
            using (var scope = provider.CreateScope())
            {
                var factory = scope.ServiceProvider.GetService<ICSharpObjectFactory>();
                var document = factory.GetDocument();
                object owner = null;
                if (!method.IsStatic)
                {
                    owner = ActivatorUtilities.CreateInstance(scope.ServiceProvider, method.DeclaringType);
                }
                method.Invoke(owner, new[] { document });
                var staticContext = CSharpStaticAnalysisContext.Default;
                var csharpObjects = factory.GetAllObjects();
                foreach (var extractor in extractors)
                {
                    extractor.ExtractMetadata(csharpObjects, staticContext);
                }
                return (staticContext, null);
            }
        }
    }
}