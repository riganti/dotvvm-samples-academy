using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class ErrorAggregatingVisitor : ResolvedControlTreeVisitor
    {
        public List<ValidationDiagnostic> Diagnostics { get; } = new List<ValidationDiagnostic>();

        public override void DefaultVisit(IResolvedTreeNode node)
        {
            try
            {
                base.DefaultVisit(node);
            }
            catch (DotvvmCompilationException exception)
            {
                DothtmlNode syntaxNode = null;
                if (exception.Tokens != null)
                {
                    var parentSyntaxNode = ((ResolvedTreeNode)node).DothtmlNode;
                    syntaxNode = parentSyntaxNode.FindNodeByPosition(exception.Tokens.First().StartPosition);
                }
                Exception innermost = exception;
                while(innermost.InnerException != null)
                {
                    innermost = innermost.InnerException;
                }
                Diagnostics.Add(new DothtmlCompilerDiagnostic(syntaxNode, innermost.Message));
            }
        }

        public override void VisitControl(ResolvedControl control)
        {
            AddErrors(control.DothtmlNode);
            AddWarnings(control.DothtmlNode);
            base.VisitControl(control);
        }

        public override void VisitPropertyBinding(ResolvedPropertyBinding propertyBinding)
        {
            var errors = propertyBinding.Binding.Errors;
            if (errors.HasErrors)
            {
                foreach (var exception in errors.Exceptions)
                {
                    Diagnostics.Add(new DothtmlCompilerDiagnostic(propertyBinding.DothtmlNode, exception.Message));
                }
            }
        }

        public override void VisitView(ResolvedTreeRoot view)
        {
            AddErrors(view.DothtmlNode);
            AddWarnings(view.DothtmlNode);
            foreach (var directive in ((DothtmlRootNode)view.DothtmlNode).Directives)
            {
                AddErrors(directive);
                AddWarnings(directive);
            }
            base.VisitView(view);
        }

        private void AddErrors(DothtmlNode node)
        {
            foreach (var error in node.NodeErrors)
            {
                Diagnostics.Add(new DothtmlCompilerDiagnostic(node, error));
            }
        }

        private void AddWarnings(DothtmlNode node)
        {
            foreach (var warning in node.NodeWarnings)
            {
                Diagnostics.Add(new DothtmlCompilerDiagnostic(node, warning, ValidationDiagnosticSeverity.Warning));
            }
        }
    }
}