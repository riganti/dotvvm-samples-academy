namespace DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions
{
    public static class CSharpMemberedTypeExtensions
    {
        public static ICSharpProperty GetAutoProperty(this ICSharpMemberedType type, string name)
        {
            var property = type.GetProperty(name);
            property.GetGetter();
            property.GetSetter();
            return property;
        }
    }
}