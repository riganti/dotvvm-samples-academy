using System;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;

namespace DotvvmAcademy.Validation.CSharp
{
    public class ReflectionMetadataNameProvider : IMetadataNameProvider<MemberInfo>
    {
        private readonly IMetadataNameFactory factory;

        public ReflectionMetadataNameProvider(IMetadataNameFactory factory)
        {
            this.factory = factory;
        }

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
            return factory.CreateFieldName(
                returnType: GetTypeName(@event.EventHandlerType),
                owner: GetTypeName(@event.DeclaringType),
                name: @event.Name);
        }

        private MetadataName GetFieldName(FieldInfo field)
        {
            return factory.CreateFieldName(
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
            return factory.CreateMethodName(
                owner: declaringType,
                name: constructor.Name,
                parameters: parameters);
        }

        private MetadataName GetMethodName(MethodInfo method)
        {
            if(!method.IsGenericMethod || method.IsGenericMethodDefinition)
            {

                var parameters = method.GetParameters()
                    .Select(p => GetTypeName(p.ParameterType))
                    .ToImmutableArray();
                return factory.CreateMethodName(
                    owner: GetTypeName(method.DeclaringType),
                    name: method.Name,
                    returnType: GetTypeName(method.ReturnType),
                    parameters: parameters);
            }
            var genericArguments = method.GetGenericArguments()
                .Select(p => GetTypeName(p))
                .ToImmutableArray();
            return factory.CreateConstructedMethodName(
                owner: GetMethodName(method.GetGenericMethodDefinition()),
                typeArguments: genericArguments);
        }

        private MetadataName GetPropertyName(PropertyInfo property)
        {
            return factory.CreateMethodName(
                returnType: GetTypeName(property.PropertyType),
                owner: GetTypeName(property.DeclaringType),
                name: property.Name);
        }

        private MetadataName GetTypeName(Type type)
        {
            if (type.IsPointer)
            {
                return factory.CreatePointerType(
                    owner: GetTypeName(type.DeclaringType));
            }
            else if (type.IsArray)
            {
                return factory.CreateArrayTypeName(
                    owner: GetTypeName(type.DeclaringType),
                    rank: type.GetArrayRank());
            }
            else if (type.IsGenericParameter)
            {
                return factory.CreateTypeParameterName(
                    owner: GetTypeName(type.DeclaringType),
                    name: type.Name);
            }
            else if (type.IsConstructedGenericType)
            {
                var genericArguments = type.GetGenericArguments()
                    .Select(t => GetTypeName(t))
                    .ToImmutableArray();
                return factory.CreateConstructedTypeName(
                    owner: GetTypeName(type.GetGenericTypeDefinition()),
                    typeArguments: genericArguments);
            }
            else if (type.IsNested)
            {
                return factory.CreateNestedTypeName(
                    owner: GetTypeName(type.DeclaringType),
                    name: type.Name,
                    arity: type.GenericTypeArguments.Length);
            }
            else
            {
                return factory.CreateTypeName(
                    @namespace: type.Namespace,
                    name: type.Name,
                    arity: type.GenericTypeArguments.Length);
            }
        }
    }
}