using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Tokenizer;
using DotVVM.Framework.Configuration;
using DotvvmAcademy.Validation;
using DotvvmAcademy.Validation.Dothtml;
using DotvvmAcademy.Validation.Dothtml.Unit;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    internal class DothtmlValidationService : IValidationService
    {
        private CourseWorkspace workspace;
        private ICodeTask task;
        private string code;
        private MetadataCollection<DothtmlIdentifier> metadata;
        private ResolvedTreeRoot root;
        private List<ValidationDiagnostic> diagnostics = new List<ValidationDiagnostic>();

        public async Task<ImmutableArray<ICodeTaskDiagnostic>> Validate(CourseWorkspace workspace, CodeTaskId id, string code)
        {
            this.workspace = workspace;
            task = await workspace.LoadCodeTask(id);
            this.code = code;
            RunValidationScript();
            ResolveTree();
            RunStaticValidation();
            return diagnostics.Select(d =>
            {
                return new CodeTaskDiagnostic
                {
                    Start = d.Location.Start,
                    End = d.Location.End,
                    Severity = CodeTaskDiagnosticSeverity.Error,
                    Message = d.Message
                };
            })
                .Cast<ICodeTaskDiagnostic>()
                .ToImmutableArray();
        }

        private void RunValidationScript()
        {
            var sourceResolver = new CourseFormatSourceResolver(workspace);
            var options = ScriptOptions.Default
                .AddReferences(
                    GetMetadataReference("DotvvmAcademy.Validation.Dothtml"),
                    GetMetadataReference("Microsoft.CSharp"),
                    GetMetadataReference("System.Runtime"),
                    GetMetadataReference("System.Private.CoreLib"),
                    GetMetadataReference("System.Linq.Expressions")) // Roslyn Issue #23573
                .AddImports("DotvvmAcademy.Validation.CSharp", "DotvvmAcademy.Validation.CSharp.Unit")
                .WithFilePath(task.Id.Path)
                .WithSourceResolver(sourceResolver);
            var runner = CSharpScript.Create(
                code: task.ValidationScript,
                options: options,
                globalsType: typeof(IDothtmlDocument))
                .CreateDelegate();
            var parser = new DothtmlIdentifierParser();
            var dothtmlObject = new DothtmlObject(parser);
            runner(dothtmlObject);
            var metadata = dothtmlObject.GetMetadata();
        }

        private void ResolveTree()
        {
            var tokenizer = new DothtmlTokenizer();
            var parser = new DothtmlParser();
            var config = DotvvmConfiguration.CreateDefault();
            var resolver = config.ServiceProvider.GetRequiredService<IControlTreeResolver>();
            tokenizer.Tokenize(code);
            var rootNode = parser.Parse(tokenizer.Tokens);
            root = (ResolvedTreeRoot)resolver.ResolveTree(rootNode, $"UserCode_{task.Id.StepId.Moniker}_{Guid.NewGuid()}.dothtml");
        }

        private void RunStaticValidation()
        {
            var aggregator = new ErrorAggregatingVisitor();
            root.Accept(aggregator);
            diagnostics.AddRange(aggregator.Diagnostics);
            var builder = ImmutableDictionary.CreateBuilder<string, IControlVisitor>();
            builder.Add(BindingVisitor.MetadataKey, new BindingVisitor());
            builder.Add(NodeExistenceVisitor.MetadataKey, new NodeExistenceVisitor());
            builder.Add(PropertyValueVisitor.MetadataKey, new PropertyValueVisitor());
            var visitors = builder.ToImmutable();
            foreach(var firstPair in metadata)
            {
                var identifier = firstPair.Key;
                foreach(var secondPair in firstPair.Value)
                {
                    var propertyKey = secondPair.Key;
                    var value = secondPair.Value;
                    throw new NotImplementedException();
                    //visitors[propertyKey].Visit(identifier, )
                }
            }
        }

        private MetadataReference GetMetadataReference(string assemblyName)
        {
            return MetadataReference.CreateFromFile(Assembly.Load(assemblyName).Location);
        }
    }
}
