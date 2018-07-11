using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Controls;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Linq;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    internal class ValidationTypeDescriptor : ITypeDescriptor
    {
        private readonly static SymbolDisplayFormat TypeDescriptorFormat = new SymbolDisplayFormat(
            globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Omitted,
            typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
            genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
            miscellaneousOptions: SymbolDisplayMiscellaneousOptions.UseErrorTypeSymbolName);

        private readonly CSharpCompilation compilation;
        private readonly ValidationTypeDescriptorFactory factory;

        public ValidationTypeDescriptor(
            ValidationTypeDescriptorFactory factory,
            ITypeSymbol typeSymbol)
        {
            this.compilation = factory.Compilation;
            this.factory = factory;
            TypeSymbol = typeSymbol;
        }

        public string Name => TypeSymbol.Name;

        public string Namespace => TypeSymbol.ContainingNamespace?.Name;

        public string Assembly => TypeSymbol.ContainingAssembly?.Name;

        public string FullName => TypeSymbol.ToDisplayString(TypeDescriptorFormat);

        public ITypeSymbol TypeSymbol { get; }

        public ControlMarkupOptionsAttribute GetControlMarkupOptionsAttribute()
        {
            var attributeType = factory.Compilation
                .GetTypeByMetadataName(typeof(ControlMarkupOptionsAttribute).FullName);
            var attribute = TypeSymbol
                .GetAttributes()
                .SingleOrDefault(a => a.AttributeClass.Equals(attributeType));
            var allowContent = (attribute?.NamedArguments
                .SingleOrDefault(p => p.Key == nameof(ControlMarkupOptionsAttribute.AllowContent))
                .Value.Value as bool?)
                .GetValueOrDefault();
            var defaultContentProperty = attribute?.NamedArguments
                .SingleOrDefault(p => p.Key == nameof(ControlMarkupOptionsAttribute.DefaultContentProperty))
                .Value.Value as string;

            return new ControlMarkupOptionsAttribute
            {
                AllowContent = allowContent,
                DefaultContentProperty = defaultContentProperty
            };
        }

        public bool IsAssignableFrom(ITypeDescriptor typeDescriptor)
        {
            if (typeDescriptor is ValidationTypeDescriptor other)
            {
                var conversion = compilation.ClassifyConversion(other.TypeSymbol, TypeSymbol);
                return conversion.Exists;
            }
            return false;
        }

        public bool IsAssignableTo(ITypeDescriptor typeDescriptor)
        {
            if (typeDescriptor is ValidationTypeDescriptor other)
            {
                other.IsAssignableFrom(this);
            }
            return false;
        }

        public bool IsEqualTo(ITypeDescriptor typeDescriptor)
        {
            if (typeDescriptor is ValidationTypeDescriptor other)
            {
                return TypeSymbol.Equals(other.TypeSymbol);
            }
            return false;
        }

        public ITypeDescriptor MakeGenericType(params ITypeDescriptor[] descriptorArguments)
        {
            if (descriptorArguments.Any(d => !(d is ValidationTypeDescriptor)))
            {
                throw new ArgumentException($"Type arguments are not {nameof(ValidationTypeDescriptor)}s.", nameof(descriptorArguments));
            }
            if (TypeSymbol is INamedTypeSymbol namedType)
            {
                ITypeSymbol[] roslynArguments = descriptorArguments
                    .Cast<ValidationTypeDescriptor>()
                    .Select(d => d.TypeSymbol)
                    .ToArray();
                namedType.Construct(roslynArguments);
            }
            throw new NotSupportedException($"'{TypeSymbol}' is not an INamedTypeSymbol.");
        }

        public ITypeDescriptor TryGetArrayElementOrIEnumerableType()
        {

            if (TypeSymbol is IArrayTypeSymbol arrayType)
            {
                return factory.Create(arrayType.ElementType);
            }
            var iEnumerableSymbol = factory.Compilation.GetTypeByMetadataName("System.Collections.Generic.IEnumerable`1");
            if (TypeSymbol is INamedTypeSymbol namedType && namedType.ConstructedFrom.Equals(iEnumerableSymbol))
            {
                return factory.Create(namedType.TypeArguments.First());
            }
            throw new NotSupportedException($"'{TypeSymbol}' is not an IArrayTypeSymbol or a bound IEnumerable symbol.");
        }

        public ITypeDescriptor TryGetPropertyType(string propertyName)
        {
            if (TypeSymbol is INamedTypeSymbol namedType)
            {
                var property = (IPropertySymbol)namedType.GetMembers(propertyName).FirstOrDefault(m => m is IPropertySymbol);
                if (property != null)
                {
                    return factory.Create(property.Type);
                }

                throw new ArgumentException($"'{TypeSymbol}' doesn't contain property with name '{propertyName}'.");
            }

            throw new NotSupportedException($"'{TypeSymbol}' is not an INamedTypeSymbol.");
        }
    }
}