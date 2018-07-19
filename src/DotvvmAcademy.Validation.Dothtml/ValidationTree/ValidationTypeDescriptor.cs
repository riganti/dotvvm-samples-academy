using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Controls;
using DotvvmAcademy.Meta;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Diagnostics;
using System.Linq;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    [DebuggerDisplay("TypeDescriptor: {FullName,nq}")]
    public class ValidationTypeDescriptor : ITypeDescriptor
    {
        private readonly CSharpCompilation compilation;
        private readonly ValidationTypeDescriptorFactory factory;

        public ValidationTypeDescriptor(
            ValidationTypeDescriptorFactory factory,
            CSharpCompilation compilation,
            ITypeSymbol typeSymbol)
        {
            this.factory = factory;
            this.compilation = compilation;
            TypeSymbol = typeSymbol;

            FullName = FullNamer.FromRoslyn(TypeSymbol);
            Assembly = TypeSymbol.ContainingAssembly.Identity.Name;
            Namespace = TypeSymbol.ContainingNamespace.ToDisplayString();
            Name = TypeSymbol.MetadataName;
        }

        public string Assembly { get; }

        public string FullName { get; }

        public string Name { get; }

        public string Namespace { get; }

        public ITypeSymbol TypeSymbol { get; }

        public bool IsAssignableFrom(ITypeDescriptor typeDescriptor)
        {
            if (typeDescriptor is ValidationTypeDescriptor other)
            {
                var conversion = compilation.ClassifyConversion(other.TypeSymbol, TypeSymbol);
                return conversion.Exists;
            }
            return false;
        }

        public bool IsAssignableFrom(Type type)
            => IsAssignableFrom(factory.Create(type));

        public bool IsAssignableTo(ITypeDescriptor typeDescriptor)
        {
            if (typeDescriptor is ValidationTypeDescriptor other)
            {
                return other.IsAssignableFrom(this);
            }
            return false;
        }

        public bool IsAssignableTo(Type type)
            => IsAssignableTo(factory.Create(type));

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
                throw new ArgumentException(
                    message: $"Type arguments are not {nameof(ValidationTypeDescriptor)}s.",
                    paramName: nameof(descriptorArguments));
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

        public override string ToString() => FullName;

        public ITypeDescriptor TryGetArrayElementOrIEnumerableType()
        {
            if (TypeSymbol is IArrayTypeSymbol arrayType)
            {
                return factory.Create(arrayType.ElementType);
            }
            var iEnumerableSymbol = compilation.GetTypeByMetadataName(WellKnownTypes.IEnumerable);
            if (TypeSymbol is INamedTypeSymbol namedType && namedType.ConstructedFrom.Equals(iEnumerableSymbol))
            {
                return factory.Create(namedType.TypeArguments.First());
            }
            throw new NotSupportedException($"'{TypeSymbol}' is not an IArrayTypeSymbol " +
                $"or a bound IEnumerable symbol.");
        }

        public ITypeDescriptor TryGetPropertyType(string propertyName)
        {
            if (TypeSymbol is INamedTypeSymbol namedType)
            {
                var property = (IPropertySymbol)namedType
                    .GetMembers(propertyName)
                    .FirstOrDefault(m => m is IPropertySymbol);
                if (property != null)
                {
                    return factory.Create(property.Type);
                }

                throw new ArgumentException($"'{TypeSymbol}' doesn't contain property with name '{propertyName}'.");
            }

            throw new NotSupportedException($"'{TypeSymbol}' is not an INamedTypeSymbol.");
        }

        ControlMarkupOptionsAttribute ITypeDescriptor.GetControlMarkupOptionsAttribute()
        {
            throw new NotImplementedException();
        }
    }
}