using DotVVM.Framework.Compilation;
using DotVVM.Framework.Controls.Infrastructure;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Runtime;
using DotvvmAcademy.CourseFormat;
using DotvvmAcademy.Meta;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace DotvvmAcademy.Web.Hosting
{
    public class EmbeddedViewBuilder : IDotvvmViewBuilder
    {
        private readonly IControlBuilderFactory builderFactory;

        private readonly ICourseEnvironment environment;

        private readonly EmbeddedViewTreeBuilder treeBuilder;

        private readonly EmbeddedViewCompiler viewCompiler;

        private readonly CourseWorkspace workspace;

        private ConcurrentDictionary<string, (Assembly viewModel, Assembly view)> assemblies
            = new ConcurrentDictionary<string, (Assembly viewModel, Assembly view)>();

        public EmbeddedViewBuilder(
            CourseWorkspace workspace,
            ICourseEnvironment environment,
            EmbeddedViewTreeBuilder treeBuilder,
            EmbeddedViewCompiler viewCompiler,
            IControlBuilderFactory builderFactory)
        {
            this.workspace = workspace;
            this.environment = environment;
            this.treeBuilder = treeBuilder;
            this.viewCompiler = viewCompiler;
            this.builderFactory = builderFactory;
        }

        public DotvvmView BuildView(IDotvvmRequestContext context)
        {
            // identify and obtain the step
            var languageMoniker = RequireMoniker(context, "Language");
            var lessonMoniker = RequireMoniker(context, "Lesson");
            var stepMoniker = RequireMoniker(context, "Step");
            var step = workspace.LoadStep(languageMoniker, lessonMoniker, stepMoniker)
                .GetAwaiter()
                .GetResult();
            if (step.EmbeddedView == null)
            {
                throw new InvalidOperationException($"Step {step.Path} does not contain an EmbeddedView.");
            }

            // compile or get the cached viewModel and view assembly
            Assembly viewModelAssembly;
            Assembly viewAssembly;
            string controlBuilderName = $"{Path.GetFileNameWithoutExtension(step.EmbeddedView.Path)}EmbeddedViewControlBuilder";
            if (assemblies.TryGetValue(step.Path, out var pair))
            {
                viewModelAssembly = pair.viewModel;
                viewAssembly = pair.view;
            }
            else
            {
                var id = Guid.NewGuid();

                // compile and emit the viewModel assembly
                var dependencies = step.EmbeddedView.Dependencies.Select(d => environment.Read(d)
                    .GetAwaiter()
                    .GetResult());
                var viewModelCompilation = CSharpCompilation.Create(
                    assemblyName: $"EmbeddedViewModel_{step.Name}_{id}",
                    syntaxTrees: dependencies.Select(d => CSharpSyntaxTree.ParseText(d)),
                    references: new[]
                    {
                            RoslynReference.FromName("mscorlib"),
                            RoslynReference.FromName("netstandard"),
                            RoslynReference.FromName("System.Private.CoreLib"),
                            RoslynReference.FromName("System.Runtime"),
                            RoslynReference.FromName("System.ValueTuple"),
                            RoslynReference.FromName("System.Collections"),
                            RoslynReference.FromName("System.Reflection"),
                            RoslynReference.FromName("System.Linq"),
                            RoslynReference.FromName("System.Linq.Expressions"),
                            RoslynReference.FromName("System.ComponentModel.Annotations"),
                            RoslynReference.FromName("System.ComponentModel.DataAnnotations"),
                            RoslynReference.FromName("DotVVM.Framework"),
                            RoslynReference.FromName("DotVVM.Core"),
                    },
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
                var viewModelAssemblyPath = $"{Path.GetTempPath()}/{viewModelCompilation.AssemblyName}.dll";
                var viewModelResult = viewModelCompilation.Emit(viewModelAssemblyPath);
                if (!viewModelResult.Success)
                {
                    throw new InvalidOperationException("The ViewModel assembly could not be emitted to disk.");
                }
                viewModelAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(viewModelAssemblyPath);

                // compile and emit the view assembly
                var viewSource = environment.Read(step.EmbeddedView.Path)
                    .GetAwaiter()
                    .GetResult();
                treeBuilder.AdditionalAssembly = viewModelAssembly;
                var viewCompilation = viewCompiler.CreateCompilation($"EmbeddedView_{step.Name}_{id}");
                (var descriptor, var factory) = viewCompiler.CompileView(
                    sourceCode: viewSource,
                    fileName: step.EmbeddedView.Path,
                    compilation: viewCompilation,
                    namespaceName: "DotvvmAcademy.Course",
                    className: controlBuilderName);
                viewCompilation = factory();
                using (var stream = new MemoryStream())
                {
                    var result = viewCompilation.Emit(stream);
                    if (!result.Success)
                    {
                        throw new InvalidOperationException("The View assembly coult be emitter to memory.");
                    }
                    stream.Position = 0;
                    viewAssembly = AssemblyLoadContext.Default.LoadFromStream(stream);
                }

                // cache the newly compiled assemblies
                pair = (viewModelAssembly, viewAssembly);
                assemblies.AddOrUpdate(step.Path, pair, (k, v) => pair);
            }

            // locate the control builder and build the control tree
            var builder = (IControlBuilder)viewAssembly.CreateInstance($"DotvvmAcademy.Course.{controlBuilderName}");
            return (DotvvmView)builder.BuildControl(builderFactory, context.Services);
        }

        private string RequireMoniker(IDotvvmRequestContext request, string monikerName)
        {
            if (!request.Parameters.TryGetValue(monikerName, out var monikerValue))
            {
                throw new InvalidOperationException($"The request is missing the CourseFormat {monikerName} moniker.");
            }
            return (string)monikerValue;
        }
    }
}