using DotvvmAcademy.Meta;
using System;
using System.Linq;
using System.Reflection;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public class CSharpDynamicContext
    {
        private readonly IMemberInfoLocator locator;
        private readonly CSharpValidationReporter reporter;

        public CSharpDynamicContext(IMemberInfoLocator locator, CSharpValidationReporter reporter)
        {
            this.locator = locator;
            this.reporter = reporter;
        }

        public dynamic GetFieldValue(object instance, string fieldName)
        {
            var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            return instance.GetType().GetField(fieldName, flags).GetValue(instance);
        }

        public dynamic GetPropertyValue(object instance, string propertyName)
        {
            var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            return instance.GetType().GetProperty(propertyName, flags).GetValue(instance);
        }

        public dynamic Instantiate(string typeName, params object[] arguments)
        {
            var type = locator.Locate(typeName).OfType<Type>().Single();
            return Activator.CreateInstance(type, arguments);
        }

        public dynamic Invoke(object instance, string name, params object[] arguments)
        {
            var flags = BindingFlags.InvokeMethod 
                | BindingFlags.Instance 
                | BindingFlags.Static 
                | BindingFlags.Public 
                | BindingFlags.NonPublic;
            return instance.GetType().InvokeMember(name, flags, Type.DefaultBinder, instance, arguments);
        }

        public void Report(string message, ValidationSeverity severity = default)
        {
            reporter.Report(message, severity: severity);
        }
    }
}