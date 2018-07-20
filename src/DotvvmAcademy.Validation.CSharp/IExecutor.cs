using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp
{
    public interface IExecutor
    {
        object CreateInstance(
            MetadataName constructorName,
            IEnumerable<object> arguments,
            out ValidationDiagnostic diagnostic);

        object GetFieldValue(
            MetadataName fieldName,
            object instance,
            out ValidationDiagnostic diagnostic);

        object GetPropertyValue(
            MetadataName propertyName,
            object instance,
            IEnumerable<object> index,
            out ValidationDiagnostic diagnostic);

        object InvokeMethod(
            MetadataName methodName,
            object instance,
            IEnumerable<object> arguments,
            out ValidationDiagnostic diagnostic);

        void SetFieldValue(
            MetadataName fieldName,
            object instance,
            object value,
            out ValidationDiagnostic diagnostic);

        void SetPropertyValue(
            MetadataName propertyName,
            object instance,
            object value,
            IEnumerable<object> index,
            out ValidationDiagnostic diagnostic);
    }
}