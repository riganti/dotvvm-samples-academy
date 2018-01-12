namespace DotvvmAcademy.Validation.CSharp.Unit
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

        public static ICSharpProperty GetAutoProperty<TType>(this ICSharpMemberedType type, string name)
        {
            var property = GetAutoProperty(type, name);
            property.Type = typeof(TType);
            return property;
        }
    }
}