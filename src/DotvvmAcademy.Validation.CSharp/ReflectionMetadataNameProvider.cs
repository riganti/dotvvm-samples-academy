using System;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;

namespace DotvvmAcademy.Validation.CSharp
{
    public class ReflectionMetadataNameProvider : IMetadataNameProvider<MemberInfo>
    {
        public MetadataName GetName(MemberInfo source)
        {
            switch (source)
            {
                case Type type:
                    return GetTypeName(type);

                case EventInfo @event:
                    return GetEventName(@event);

                case FieldInfo field:
                    return GetFieldName(field);

                case MethodInfo method:
                    return GetMethodName(method);

                case ConstructorInfo constructor:
                    return GetConstructorName(constructor);

                case PropertyInfo property:
                    return GetPropertyName(property);

                default:
                    throw new ArgumentException($"MemberInfo inheritor {source.GetType().Name} is not supported by {nameof(ReflectionMetadataNameProvider)}.");
            }
        }

        private MetadataName GetEventName(EventInfo @event)
        {
            return MetadataName.CreateFieldOrEventName(
                returnType: GetTypeName(@event.EventHandlerType),
                owner: GetTypeName(@event.DeclaringType),
                name: @event.Name);
        }

        private MetadataName GetFieldName(FieldInfo field)
        {
            return MetadataName.CreateFieldOrEventName(
                returnType: GetTypeName(field.FieldType),
                owner: GetTypeName(field.DeclaringType),
                name: field.Name);
        }

        private MetadataName GetConstructorName(ConstructorInfo constructor)
        {
            var declaringType = GetTypeName(constructor.DeclaringType);
            var parameters = constructor.GetParameters()
                .Select(p => GetTypeName(p.ParameterType))
                .ToImmutableArray();
            return MetadataName.CreateMethodName(
                owner: declaringType,
                name: constructor.Name,
                parameters: parameters);
        }

        private MetadataName GetMethodName(MethodInfo method)
        {
            var parameters = method.GetParameters()
                .Select(p => GetTypeName(p.ParameterType))
                .ToImmutableArray();
            var genericArguments = method.GetGenericArguments()
                .Select(p => GetTypeName(p))
                .ToImmutableArray();
            return MetadataName.CreateMethodName(
                owner: GetTypeName(method.DeclaringType),
                name: method.Name,
                returnType: GetTypeName(method.ReturnType),
                parameters: parameters,
                typeParameters: genericArguments);
        }

        private MetadataName GetPropertyName(PropertyInfo property)
        {
            return MetadataName.CreatePropertyName(
                returnType: GetTypeName(property.PropertyType),
                owner: GetTypeName(property.DeclaringType),
                name: property.Name);
        }

        private MetadataName GetTypeName(Type type)
        {
            if (type.IsPointer)
            {
                return MetadataName.CreatePointerType(GetTypeName(type.DeclaringType));
            }
            else if (type.IsArray)
            {
                return MetadataName.CreateArrayTypeName(GetTypeName(type.DeclaringType));
            }
            else if (type.IsGenericType)
            {
                return MetadataName.CreateTypeParameterName(type.FullName);
            }
            else if (type.IsConstructedGenericType)
            {
                var genericArguments = type.GetGenericArguments()
                    .Select(t => GetTypeName(t))
                    .ToImmutableArray();
                return MetadataName.CreateConstructedTypeName(
                    owner: GetTypeName(type.GetGenericTypeDefinition()),
                    typeArguments: genericArguments);
            }
            else if (type.IsNested)
            {
                return MetadataName.CreateNestedTypeName(
                    owner: GetTypeName(type.DeclaringType),
                    name: type.Name,
                    arity: type.GenericTypeArguments.Length);
            }
            else
            {
                return MetadataName.CreateTypeName(
                    @namespace: type.Namespace,
                    name: type.Name,
                    arity: type.GenericTypeArguments.Length);
            }
        }
    }
}