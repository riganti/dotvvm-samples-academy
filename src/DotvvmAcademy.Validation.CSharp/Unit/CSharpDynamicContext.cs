using DotvvmAcademy.Meta;
using System;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public class CSharpDynamicContext
    {
        private readonly MemberInfoLocator locator;
        private readonly ValidationReporter reporter;

        public CSharpDynamicContext(MemberInfoLocator locator, ValidationReporter reporter)
        {
            this.locator = locator;
            this.reporter = reporter;
        }

        public dynamic Instantiate(string typeName, params object[] arguments)
        {
            var type = locator.Locate(typeName).OfType<Type>().Single();
            return Activator.CreateInstance(type, arguments);
        }

        public void Report(string message, ValidationSeverity severity = default)
            => reporter.Report(message, severity: severity);
    }
}