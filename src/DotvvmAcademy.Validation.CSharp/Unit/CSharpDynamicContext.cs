using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public delegate void DynamicValidationAction(CSharpDynamicContext context);

    public class CSharpDynamicContext
    {
        private readonly MemberInfoLocator locator;

        public CSharpDynamicContext(MemberInfoLocator locator)
        {
            this.locator = locator;
        }

        public List<ValidationDiagnostic> Diagnostics { get; } = new List<ValidationDiagnostic>();

        public dynamic Instantiate(ICSharpType type, params object[] arguments)
        {
            if (locator.TryLocate(((CSharpObject)type).Name, out var info) && info is Type typeInfo)
            {
                return Activator.CreateInstance(typeInfo, arguments);
            }
            else
            {
                throw new ArgumentException("Type doesn't exist in user's code.", nameof(type));
            }
        }

        public void Report(string message, ValidationDiagnosticSeverity severity = ValidationDiagnosticSeverity.Error)
        {
            Diagnostics.Add(new DynamicValidationDiagnostic(message, severity));
        }

        private class DynamicValidationDiagnostic : ValidationDiagnostic
        {
            public DynamicValidationDiagnostic(string message, ValidationDiagnosticSeverity severity)
            {
                Message = message;
                Severity = severity;
            }

            public override string Id => "DYNAMIC";

            public override ValidationDiagnosticLocation Location { get; } = ValidationDiagnosticLocation.Global;

            public override string Message { get; }

            public override string Name { get; } = "Dynamic Validation Diagnostic";

            public override ValidationDiagnosticSeverity Severity { get; }
        }
    }
}