using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DotvvmAcademy.Validation.CSharp
{
    public class ReflectionExecutor : IExecutor
    {
        private readonly MemberInfoLocator locator;

        public ReflectionExecutor(MemberInfoLocator locator)
        {
            this.locator = locator;
        }

        public object CreateInstance(MetadataName constructorName, IEnumerable<object> arguments, out ValidationDiagnostic diagnostic)
        {
            if (!TryLocateMember<ConstructorInfo>(constructorName, out var constructorInfo, out diagnostic))
            {
                return null;
            }

            try
            {
                return constructorInfo.Invoke(arguments.ToArray());
            }
            catch (Exception e)
            {
                diagnostic = new ValidationDiagnostic(
                    id: "TEMP14",
                    message: $"Constructor of {constructorName.Owner} threw {e.GetType().Name}.",
                    severity: ValidationDiagnosticSeverity.Error);
                return null;
            }
        }

        public object GetFieldValue(MetadataName fieldName, object instance, out ValidationDiagnostic diagnostic)
        {
            if (!TryLocateMember<FieldInfo>(fieldName, out var fieldInfo, out diagnostic))
            {
                return null;
            }

            try
            {
                return fieldInfo.GetValue(instance);
            }
            catch (Exception e)
            {
                diagnostic = new ValidationDiagnostic(
                    id: "TEMP15",
                    message: $"{e.GetType().Name} has been thrown while getting the value of field {fieldName}.",
                    severity: ValidationDiagnosticSeverity.Error);
                return null;
            }
        }

        public object GetPropertyValue(MetadataName propertyName, object instance, IEnumerable<object> index, out ValidationDiagnostic diagnostic)
        {
            if (!TryLocateMember<PropertyInfo>(propertyName, out var propertyInfo, out diagnostic))
            {
                return null;
            }

            try
            {
                return propertyInfo.GetValue(instance, index?.ToArray());
            }
            catch (Exception e)
            {
                diagnostic = new ValidationDiagnostic(
                    id: "TEMP16",
                    message: $"{e.GetType().Name} has been thrown while getting the value of property {propertyName}.",
                    severity: ValidationDiagnosticSeverity.Error);
                return null;
            }
        }

        public object InvokeMethod(MetadataName methodName, object instance, IEnumerable<object> arguments, out ValidationDiagnostic diagnostic)
        {
            if (!TryLocateMember<MethodInfo>(methodName, out var methodInfo, out diagnostic))
            {
                return null;
            }

            try
            {
                return methodInfo.Invoke(instance, arguments.ToArray());
            }
            catch (Exception e)
            {
                diagnostic = new ValidationDiagnostic(
                    id: "TEMP13",
                    message: $"Method {methodName} threw {e.GetType().Name} during its execution.",
                    severity: ValidationDiagnosticSeverity.Error);
                return null;
            }
        }

        public void SetFieldValue(MetadataName fieldName, object instance, object value, out ValidationDiagnostic diagnostic)
        {
            if(!TryLocateMember<FieldInfo>(fieldName, out var fieldInfo, out diagnostic))
            {
                return;
            }

            try
            {
                fieldInfo.SetValue(instance, value);
            }
            catch(Exception e)
            {
                diagnostic = new ValidationDiagnostic(
                    id: "TEMP18",
                    message: $"{e.GetType().Name} has been thrown while setting the value of field {fieldName}.",
                    severity: ValidationDiagnosticSeverity.Error);
            }
        }

        public void SetPropertyValue(MetadataName propertyName, object instance, object value, IEnumerable<object> index, out ValidationDiagnostic diagnostic)
        {
            if (!TryLocateMember<PropertyInfo>(propertyName, out var propertyInfo, out diagnostic))
            {
                return;
            }

            try
            {
                propertyInfo.SetValue(instance, value, index?.ToArray());
            }
            catch (Exception e)
            {
                diagnostic = new ValidationDiagnostic(
                    id: "TEMP17",
                    message: $"{e.GetType().Name} has been thrown while setting the value of property {propertyName}.",
                    severity: ValidationDiagnosticSeverity.Error);
            }
        }

        private bool TryLocateMember<TMemberInfo>(MetadataName name, out TMemberInfo member, out ValidationDiagnostic diagnostic)
            where TMemberInfo : MemberInfo
        {
            if (!locator.TryLocate(name, out var memberInfo))
            {
                member = null;
                diagnostic = new ValidationDiagnostic("TEMP11", $"{nameof(MemberInfo)} {name} could not be located.", ValidationDiagnosticSeverity.Error);
                return false;
            }

            if (!(memberInfo is TMemberInfo castInfo))
            {
                member = null;
                diagnostic = new ValidationDiagnostic("TEMP12", $"{nameof(MemberInfo)} {name} is not a {typeof(TMemberInfo).Name}.", ValidationDiagnosticSeverity.Error);
                return false;
            }

            member = castInfo;
            diagnostic = default(ValidationDiagnostic);
            return true;
        }
    }
}