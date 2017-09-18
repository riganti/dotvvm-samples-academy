using System;

namespace DotvvmAcademy.Validation.CSharp
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowedIdentifiersAttribute : Attribute
    {
        public AllowedIdentifiersAttribute(params string[] allowedIdentifiers)
        {
            AllowedIdentifiers = allowedIdentifiers;
        }

        public string[] AllowedIdentifiers { get; }
    }
}