using System;

namespace DotvvmAcademy.Validation
{
    public static class ValidationContextExtensions
    {
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
    }
}