using System;

namespace DotvvmAcademy.Controls
{
    [AttributeUsage(AttributeTargets.Field)]
    public class MonacoLanguageAttribute : Attribute
    {
        public MonacoLanguageAttribute(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}