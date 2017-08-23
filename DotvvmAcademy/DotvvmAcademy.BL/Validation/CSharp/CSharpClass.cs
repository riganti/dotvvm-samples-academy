using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DotvvmAcademy.BL.Validation.CSharp
{
    public sealed class CSharpClass : CSharpObject<ClassDeclarationSyntax>
    {
        internal CSharpClass(CSharpValidate validate, ClassDeclarationSyntax node, bool isActive = true) : base(validate, node, isActive)
        {
            if (!IsActive) return;

            Symbol = Validate.Model.GetDeclaredSymbol(Node);
        }

        public static CSharpClass Inactive => new CSharpClass(null, null, false);

        public INamedTypeSymbol Symbol { get; }

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

        public CSharpClassInstance Instance(params object[] constructorArguments)
        {
            if (!IsActive) return CSharpClassInstance.Inactive;
            object instance;
            try
            {
                instance = Validate.Assembly.CreateInstance(Symbol.ToString(), false, BindingFlags.CreateInstance, null, constructorArguments, null, null);
                if (instance == null)
                {
                    AddError("This class could not be instantiated. Assembly.CreateInstance returned null.");
                    return CSharpClassInstance.Inactive;
                }
            }
            catch (Exception e)
            {
                AddError($"This class could not be instantiated. Exception message: '{e.Message}'.");
                return CSharpClassInstance.Inactive;
            }
            return new CSharpClassInstance(Validate, Node, instance);
        }

        public CSharpMethod Method(string name, Type returnType, params Type[] parameters)
        {
            if (!IsActive) return CSharpMethod.Inactive;

            var errorMessage = $"This class is missing the '{GetMethodSignature(name, returnType.Name, parameters.Select(p => p.Name).ToArray())}' method.";
            var methods = Node.Members
                .OfType<MethodDeclarationSyntax>()
                .Where(m => m.Identifier.ValueText == name)
                .ToList();
            if (methods.Count == 0)
            {
                AddError(errorMessage);
                return CSharpMethod.Inactive;
            }

            var methodSymbols = methods
                .Select(m => Validate.Model.GetDeclaredSymbol(m))
                .Where(s => s.ReturnType.EqualsType(returnType))
                .ToList();
            if (methodSymbols.Count == 0)
            {
                AddError(errorMessage);
                return CSharpMethod.Inactive;
            }

            var methodSymbol = methodSymbols.SingleOrDefault(s => ValidateParameterTypes(s.Parameters, parameters));
            if (methodSymbol == default(IMethodSymbol))
            {
                AddError(errorMessage);
                return CSharpMethod.Inactive;
            }

            return new CSharpMethod(Validate, methods.Single(m => Validate.Model.GetDeclaredSymbol(m) == methodSymbol));
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

        protected override void AddError(string message) => AddError(message, Node.Identifier.Span.Start, Node.Identifier.Span.End);

        private string GetMethodSignature(string name, string returnType, params string[] parameterTypes)
        {
            var sb = new StringBuilder();
            sb.Append($"{returnType} {name}(");
            for (int i = 0; i < parameterTypes.Length; i++)
            {
                sb.Append(parameterTypes[i]);
                if (i != parameterTypes.Length - 1)
                {
                    sb.Append(", ");
                }
            }
            sb.Append(')');
            return sb.ToString();
        }

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

        private bool ValidateParameterTypes(ImmutableArray<IParameterSymbol> parameterSymbols, Type[] parameterTypes)
        {
            if (parameterSymbols.Length != parameterTypes.Length)
            {
                return false;
            }

            for (int i = 0; i < parameterSymbols.Length; i++)
            {
                var symbol = parameterSymbols[i];
                var type = parameterTypes[i];
                if (!symbol.Type.EqualsType(type))
                {
                    return false;
                }
            }

            return true;
        }
    }
}