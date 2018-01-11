using System;
using System.Reflection;

namespace DotvvmAcademy.Controls
{
    [AttributeUsage(AttributeTargets.Field)]
    public class MonacoValueAttribute : Attribute
    {
        public MonacoValueAttribute(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static string GetValue<TEnum>(TEnum instance)
        {
            return typeof(TEnum)
            .GetField(instance.ToString())
            .GetCustomAttribute<MonacoValueAttribute>()
            .Value;
        }
    }
}