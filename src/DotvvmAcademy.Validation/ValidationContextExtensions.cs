using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation
{
    public static class ValidationContextExtensions
    {
        public static TValue GetItem<TValue>(this ValidationContext context, string key)
        {
            var item = context.GetItem(key);
            if (item is TValue value)
            {
                return value;
            }

            return default(TValue);
        }

        public static TValue GetRequiredItem<TValue>(this ValidationContext context, string key)
        {
            return (TValue)GetRequiredItem(context, key, typeof(TValue));
        }

        public static object GetRequiredItem(this ValidationContext context, string key, Type itemType)
        {
            var item = context.GetItem(key);
            if (item == null)
            {
                throw new InvalidOperationException($"Missing required item '{key}' of type '{itemType.Name}'.");
            }

            if (!itemType.IsAssignableFrom(item.GetType()))
            {
                throw new InvalidOperationException($"Required item '{key}' must be assignable to type '{itemType.Name}'.");
            }

            return item;
        }

        public static void ReportDiagnostic(this ValidationContext context, ValidationDiagnostic diagnostic)
        {
            var diagnostics = GetRequiredItem<IList<ValidationDiagnostic>>(context, ValidationContext.DiagnosticsKey);
            diagnostics.Add(diagnostic);
        }
    }
}