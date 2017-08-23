using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;

namespace DotvvmAcademy.BL.Validation.CSharp
{
    public sealed class CSharpClass : CSharpValidationObject<ClassDeclarationSyntax>
    {
        internal CSharpClass(CSharpValidate validate, ClassDeclarationSyntax node, bool isActive = true) : base(validate, node, isActive)
        {
        }

        public static CSharpClass Inactive => new CSharpClass(null, null, false);

        public CSharpProperty AutoProperty<TProperty>(string name,
            CSharpAccessModifier access = CSharpAccessModifier.Public,
            bool hasGetter = true,
            CSharpAccessModifier getterAccess = CSharpAccessModifier.Public,
            bool hasSetter = true,
            CSharpAccessModifier setterAccess = CSharpAccessModifier.Public)
        {
            var property = Property<TProperty>(name);
            ValidateAutoProperty(property, access, hasGetter, getterAccess, hasSetter, setterAccess);
            return property;
        }

        public CSharpProperty AutoProperty(string typeFullName, string name,
            CSharpAccessModifier access = CSharpAccessModifier.Public,
            bool hasGetter = true,
            CSharpAccessModifier getterAccess = CSharpAccessModifier.Public,
            bool hasSetter = true,
            CSharpAccessModifier setterAccess = CSharpAccessModifier.Public)
        {
            var property = Property(typeFullName, name);
            ValidateAutoProperty(property, access, hasGetter, getterAccess, hasSetter, setterAccess);
            return property;
        }

        public CSharpClassInstance Instance()
        {
            if (!IsActive) return CSharpClassInstance.Inactive;
            return CSharpClassInstance.Inactive;
        }

        public void Method(string name, Type returnType, params Type[] parameters)
        {
            if (!IsActive) return;
        }

        public CSharpProperty Property<TProperty>(string name)
        {
            var type = typeof(TProperty);
            return Property(type.FullName, name, s => s.EqualsType(type));
        }

        public CSharpProperty Property(string typeFullName, string name)
        {
            return Property(typeFullName, name, s => s.EqualsTypeFullName(typeFullName));
        }

        private void AddError(string message) => AddError(message, Node.Identifier.Span.Start, Node.Identifier.Span.End);

        private CSharpProperty Property(string typeFullname, string name, Func<ITypeSymbol, bool> typeCheck)
        {
            if (!IsActive) return CSharpProperty.Inactive;

            var properties = Node.Members.OfType<PropertyDeclarationSyntax>().Where(p => p.Identifier.ValueText == name).ToList();
            if (properties.Count > 1)
            {
                AddError($"This class should not contain multiple properties called '{name}'.");
                return CSharpProperty.Inactive;
            }

            if (properties.Count == 0)
            {
                AddError($"This class is missing the '{name}' property.");
                return CSharpProperty.Inactive;
            }

            var property = properties.Single();
            if (!typeCheck(Validate.Model.GetTypeInfo(property.Type).ConvertedType))
            {
                AddError($"This property should be of type: '{typeFullname}'.", property.Type.Span.Start, property.Type.Span.End);
                return CSharpProperty.Inactive;
            }

            return new CSharpProperty(Validate, property);
        }

        private void ValidateAutoProperty(CSharpProperty property, CSharpAccessModifier access, 
            bool hasGetter, CSharpAccessModifier getterAccess, bool hasSetter, CSharpAccessModifier setterAccess)
        {
            property.AccessModifier(access);
            if (hasGetter)
            {
                property.Getter(getterAccess);
            }
            else
            {
                property.NoGetter();
            }

            if (hasSetter)
            {
                property.Setter(setterAccess);
            }
            else
            {
                property.NoSetter();
            }
        }
    }
}