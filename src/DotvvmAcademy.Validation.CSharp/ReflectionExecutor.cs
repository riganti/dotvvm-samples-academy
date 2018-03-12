using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DotvvmAcademy.Validation.CSharp
{
    public class ReflectionExecutor : IExecutor
    {
        public static readonly ValidationDiagnosticDescriptor BadMemberType
            = new ValidationDiagnosticDescriptor(
                id: "TEMP12",
                name: "Bad Member Name",
                message: "MemberInfo {1} is not a {1}.",
                severity: ValidationDiagnosticSeverity.Error);

        public static readonly ValidationDiagnosticDescriptor ConstructorException
            = new ValidationDiagnosticDescriptor(
                id: "TEMP14",
                name: "Constructor Exception",
                message: "Constructor of {0} threw exception of type {1}.",
                severity: ValidationDiagnosticSeverity.Error);

        public static readonly ValidationDiagnosticDescriptor GetFieldException
            = new ValidationDiagnosticDescriptor(
                id: "TEMP15",
                name: "Get Field Exception",
                message: "{0} has been thrown while getting the value of field {1}.",
                severity: ValidationDiagnosticSeverity.Error);

        public static readonly ValidationDiagnosticDescriptor GetPropertyException
            = new ValidationDiagnosticDescriptor(
                id: "TEMP16",
                name: "Get Property Exception",
                message: "{0} has been thrown while getting the value of property {1}.",
                severity: ValidationDiagnosticSeverity.Error);

        public static readonly ValidationDiagnosticDescriptor MemberNotLocated
            = new ValidationDiagnosticDescriptor(
                id: "TEMP11",
                name: "Member Could Not Be Located",
                message: "MemberInfo of {1} could not be located.",
                severity: ValidationDiagnosticSeverity.Error);

        public static readonly ValidationDiagnosticDescriptor MethodException
            = new ValidationDiagnosticDescriptor(
                id: "TEMP13",
                name: "Method Invokation Exception",
                message: "Method {0} threw {1} during its execution.",
                severity: ValidationDiagnosticSeverity.Error);

        public static readonly ValidationDiagnosticDescriptor SetFieldException
            = new ValidationDiagnosticDescriptor(
                id: "TEMP18",
                name: "Set Field Exception",
                message: "{0} has been thrown while setting the value of field {1}.",
                severity: ValidationDiagnosticSeverity.Error);

        public static readonly ValidationDiagnosticDescriptor SetPropertyException
            = new ValidationDiagnosticDescriptor(
                id: "TEMP17",
                name: "Set Property Exception",
                message: "{0} has been thrown while setting the value of property {1}.",
                severity: ValidationDiagnosticSeverity.Error);

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
                diagnostic = new ExceptionValidationDiagnostic(ConstructorException, e, constructorName);
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
                diagnostic = new ExceptionValidationDiagnostic(GetFieldException, e, fieldName);
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
                diagnostic = new ExceptionValidationDiagnostic(GetPropertyException, e, propertyName);
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
                diagnostic = new ExceptionValidationDiagnostic(MethodException, e, methodName);
                return null;
            }
        }

        public void SetFieldValue(MetadataName fieldName, object instance, object value, out ValidationDiagnostic diagnostic)
        {
            if (!TryLocateMember<FieldInfo>(fieldName, out var fieldInfo, out diagnostic))
            {
                return;
            }

            try
            {
                fieldInfo.SetValue(instance, value);
            }
            catch (Exception e)
            {
                diagnostic = new ExceptionValidationDiagnostic(SetFieldException, e, fieldName);
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
                diagnostic = new ExceptionValidationDiagnostic(SetPropertyException, e, propertyName);
            }
        }

        private bool TryLocateMember<TMemberInfo>(MetadataName name, out TMemberInfo member, out ValidationDiagnostic diagnostic)
            where TMemberInfo : MemberInfo
        {
            if (!locator.TryLocate(name, out var memberInfo))
            {
                member = null;
                diagnostic = ValidationDiagnostic.Create(MemberNotLocated, null, name);
                return false;
            }

            if (!(memberInfo is TMemberInfo castInfo))
            {
                member = null;
                diagnostic = ValidationDiagnostic.Create(BadMemberType, null, name, nameof(TMemberInfo));
                return false;
            }

            member = castInfo;
            diagnostic = default(ValidationDiagnostic);
            return true;
        }
    }
}