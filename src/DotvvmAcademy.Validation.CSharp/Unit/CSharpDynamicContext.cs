using System;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public class CSharpDynamicContext
    {
        private readonly MemberInfoLocator locator;
        private readonly MetadataNameParser parser;
        private readonly ValidationReporter reporter;

        public CSharpDynamicContext(MemberInfoLocator locator, MetadataNameParser parser, ValidationReporter reporter)
        {
            this.locator = locator;
            this.parser = parser;
            this.reporter = reporter;
        }

        public dynamic Instantiate(string type, params object[] arguments)
        {
            var name = parser.Parse(type);
            if (locator.TryLocate(name, out var info) && info is Type typeInfo)
            {
                return Activator.CreateInstance(typeInfo, arguments);
            }
            else
            {
                throw new ArgumentException($"Type '{type}' could not be found.", nameof(type));
            }
        }

        public void Report(string message, ValidationSeverity severity = default)
            => reporter.Report(message, severity: severity);
    }
}