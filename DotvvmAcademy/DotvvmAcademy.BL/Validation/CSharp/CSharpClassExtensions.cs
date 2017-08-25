namespace DotvvmAcademy.BL.Validation.CSharp
{
    public static class CSharpClassExtensions
    {
        public static CSharpProperty AutoProperty<TPropertyType>(this CSharpClass c, string name,
            CSharpAccessModifier access = CSharpAccessModifier.Public,
            bool hasGetter = true,
            CSharpAccessModifier getterAccess = CSharpAccessModifier.Public,
            bool hasSetter = true,
            CSharpAccessModifier setterAccess = CSharpAccessModifier.Public)
        {
            if (!c.IsActive) return CSharpProperty.Inactive;

            return c.AutoProperty(typeof(TPropertyType).GetDescriptor(c.Validate), name, access, hasGetter, getterAccess, hasSetter, setterAccess);
        }

        public static CSharpProperty AutoProperty(this CSharpClass c, CSharpTypeDescriptor type, string name,
            CSharpAccessModifier access = CSharpAccessModifier.Public,
            bool hasGetter = true,
            CSharpAccessModifier getterAccess = CSharpAccessModifier.Public,
            bool hasSetter = true,
            CSharpAccessModifier setterAccess = CSharpAccessModifier.Public)
        {
            if (!c.IsActive || !type.IsActive) return CSharpProperty.Inactive;

            var property = c.Property(name);
            property.Type(type);
            property.AccessModifier(access);
            if (hasGetter) property.Getter(getterAccess);
            else property.NoGetter();
            if (hasSetter) property.Setter(setterAccess);
            else property.NoSetter();

            return property;
        }

        public static CSharpClassInstance Instance(this CSharpClass c, params object[] constructorArguments)
            => c.Validate.Instance(c, constructorArguments);

        public static CSharpProperty Property<TPropertyType>(this CSharpClass c, string name)
        {
            if (!c.IsActive) return CSharpProperty.Inactive;

            var property = c.Property(name);
            property.Type(typeof(TPropertyType).GetDescriptor(c.Validate));

            return property;
        }
    }
}