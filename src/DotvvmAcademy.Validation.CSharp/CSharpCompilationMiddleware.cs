using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.CSharp
{
    public class CSharpCompilationMiddleware : IValidationMiddleware
    {
        public const string AdditionalMetadataReferencesKey = "AdditionalMetadataReferences";
        public const string AssemblyNameKey = "AssemblyName";
        public const string CSharpCompilationKey = "CSharpCompilation";
        public const string CSharpCompilationOptionsKey = "CompilationOptions";
        public const string SourcesKey = "Sources";

        public Task InvokeAsync(ValidationContext context, ValidationDelegate next)
        {
            var sources = GetSources(context);
            var trees = sources.Select(s => CSharpSyntaxTree.ParseText(s));
            var compilation = CSharpCompilation.Create(
                assemblyName: GetAssemblyName(context),
                syntaxTrees: trees,
                references: GetReferences(context),
                options: GetCompilationOptions(context));
            context.SetItem(CSharpCompilationKey, compilation);
            return next(context);
        }

        private string GetAssemblyName(ValidationContext context)
            => context.GetItem<string>(AssemblyNameKey) ?? $"<Validation>{Guid.NewGuid()}";

        private CSharpCompilationOptions GetCompilationOptions(ValidationContext context)
        {
            var options = context.GetItem<CSharpCompilationOptions>(CSharpCompilationOptionsKey);
            return options ?? new CSharpCompilationOptions(
                outputKind: OutputKind.DynamicallyLinkedLibrary,
                reportSuppressedDiagnostics: true);
        }

        private MetadataReference GetReference(string assemblyName)
        {
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(assemblyName));
            return MetadataReference.CreateFromFile(assembly.Location);
        }

        private ImmutableArray<MetadataReference> GetReferences(ValidationContext context)
        {
            var isNetCore = AppContext.TargetFrameworkName.Contains("netcoreapp");
            var builder = ImmutableArray.CreateBuilder<MetadataReference>();
            var corlib = isNetCore
                ? GetReference("System.Private.Corlib")
                : GetReference("mscorlib");
            builder.Add(corlib);
            builder.Add(GetReference("System.Runtime"));
            builder.AddRange(context.GetItem<ImmutableArray<MetadataReference>>(AdditionalMetadataReferencesKey));
            return builder.ToImmutable();
        }

        private ImmutableArray<string> GetSources(ValidationContext context)
            => context.GetRequiredItem<ImmutableArray<string>>(SourcesKey);
    }
}