namespace DotvvmAcademy.Validation.CSharp
{
    public static class ExecutorExtensions
    {
        public static object GetPropertyValue(this IExecutor executor, MetadataName propertyName, object instance, out ValidationDiagnostic diagnostic)
        {
            return executor.GetPropertyValue(propertyName, instance, null, out diagnostic);
        }

        public static void SetPropertyValue(this IExecutor executor, MetadataName propertyName, object instance, object value, out ValidationDiagnostic diagnostic)
        {
            executor.SetPropertyValue(propertyName, instance, value, out diagnostic);
        }

    }
}