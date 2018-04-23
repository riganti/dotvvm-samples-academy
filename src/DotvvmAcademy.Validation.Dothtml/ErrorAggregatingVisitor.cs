using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class ErrorAggregatingVisitor : ResolvedControlTreeVisitor
    {
        public List<ValidationDiagnostic> Errors { get; } = new List<ValidationDiagnostic>();

        public override void VisitControl(ResolvedControl control)
        {
            if (control.DothtmlNode.HasNodeErrors)
            {
                AddErrors(control.DothtmlNode);
            }
            base.VisitControl(control);
        }

        public override void VisitView(ResolvedTreeRoot view)
        {
            if (view.DothtmlNode.HasNodeErrors)
            {
                AddErrors(view.DothtmlNode);
            }
            foreach (var directive in ((DothtmlRootNode)view.DothtmlNode).Directives)
            {
                if (directive.HasNodeErrors)
                {
                    AddErrors(directive);
                }
            }
            base.VisitView(view);
        }

        public override void VisitPropertyBinding(ResolvedPropertyBinding propertyBinding)
        {
            var errors = propertyBinding.Binding.Errors;
            if (errors.HasErrors)
            {
                foreach(var exception in errors.Exceptions)
                {
                    Errors.Add(new DothtmlValidationDiagnostic(propertyBinding.DothtmlNode, exception.Message));
                }
            }
        }

        private void AddErrors(DothtmlNode node)
        {
            foreach (var error in node.NodeErrors)
            {
                Errors.Add(new DothtmlValidationDiagnostic(node, error));
            }
        }
    }
}
