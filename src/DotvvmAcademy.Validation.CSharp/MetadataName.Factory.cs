using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp
{
    public sealed partial class MetadataName
    {
        public static MetadataName CreateArrayTypeName(MetadataName owner)
        {
            ValidateOwner(owner);

            string BuildArrayTypeName(NamingConvention convention)
            {
                return new Builder(convention)
                    .Append(owner)
                    .AppendArraySyntax()
                    .ToString();
            }

            return new MetadataName(
                build: BuildArrayTypeName,
                owner: owner)
            {
                IsArrayType = true,
                IsType = true
            };
        }

        public static MetadataName CreateConstructedTypeName(MetadataName owner, ImmutableArray<MetadataName> genericArguments)
        {
            ValidateOwner(owner);
            ValidateGenericArguments(genericArguments, owner);

            string BuildConstructedTypeName(NamingConvention convention)
            {
                return new Builder(convention)
                    .Append(owner)
                    .AppendGeneric(genericArguments)
                    .ToString();
            }

            return new MetadataName(
                build: BuildConstructedTypeName,
                owner: owner,
                genericArguments: genericArguments)
            {
                IsType = true,
                IsConstructedType = true
            };
        }

        public static MetadataName CreateFieldOrEventName(MetadataName returnType, MetadataName owner, string name)
        {
            ValidateReturnType(returnType);
            ValidateOwner(owner);
            ValidateName(name);

            string BuildFieldOrEventName(NamingConvention convention)
            {
                var builder = new Builder(convention);
                if (convention.MemberOptions.HasFlag(NamingConventionMemberOptions.IncludeReturnType))
                {
                    builder.AppendReturnType(returnType);
                }

                if (convention.MemberOptions.HasFlag(NamingConventionMemberOptions.IncludeOwner))
                {
                    builder.Append(owner);
                }
                builder.AppendMember(name);
                return builder.ToString();
            }

            return new MetadataName(
                build: BuildFieldOrEventName,
                returnType: returnType,
                name: name,
                owner: owner)
            {
                IsMember = true
            };
        }

        public static MetadataName CreateGenericParameterTypeName(string name)
        {
            ValidateName(name);

            string BuildGenericParameterTypeName(NamingConvention convention)
            {
                return name;
            }

            return new MetadataName(
                build: BuildGenericParameterTypeName,
                name: name)
            {
                IsType = true,
                IsGenericParameterType = true
            };
        }

        public static MetadataName CreateMethodName(MetadataName owner, string name, MetadataName returnType = null,
            ImmutableArray<MetadataName> genericParameters = default(ImmutableArray<MetadataName>),
            ImmutableArray<MetadataName> parameters = default(ImmutableArray<MetadataName>))
        {
            returnType = returnType ?? CreateTypeName("System", "Void");

            ValidateOwner(owner);
            ValidateName(name);
            ValidateReturnType(returnType);
            ValidateGenericParameters(genericParameters, owner);
            ValidateParameters(parameters);

            string BuildMethodName(NamingConvention convention)
            {
                var builder = new Builder(convention);
                if (convention.MemberOptions.HasFlag(NamingConventionMemberOptions.IncludeReturnType))
                {
                    builder.AppendReturnType(returnType);
                }

                if (convention.MemberOptions.HasFlag(NamingConventionMemberOptions.IncludeOwner))
                {
                    builder.Append(owner);
                }

                builder.AppendMember(name);

                if (convention.MemberOptions.HasFlag(NamingConventionMemberOptions.IncludeGenericParameters))
                {
                    builder.AppendGeneric(genericParameters);
                }

                if (convention.MemberOptions.HasFlag(NamingConventionMemberOptions.IncludeParameters))
                {
                    builder.AppendParameters(parameters);
                }
                return builder.ToString();
            }

            return new MetadataName(
                build: BuildMethodName,
                owner: owner,
                name: name,
                returnType: returnType,
                genericParameters: genericParameters,
                parameters: parameters)
            {
                IsMember = true
            };
        }

        public static MetadataName CreateNestedTypeName(MetadataName owner, string name, int arity = 0)
        {
            ValidateOwner(owner);
            ValidateName(name);
            ValidateArity(arity);

            string BuildNestedTypeName(NamingConvention convention)
            {
                return new Builder(convention)
                    .Append(owner)
                    .AppendNestedType(name)
                    .AppendArity(arity)
                    .ToString();
            }

            return new MetadataName(
                build: BuildNestedTypeName,
                owner: owner,
                arity: arity)
            {
                IsType = true,
                IsNestedType = true
            };
        }

        public static MetadataName CreatePointerType(MetadataName owner)
        {
            ValidateOwner(owner);

            string BuildPointerType(NamingConvention convention)
            {
                return new Builder(convention)
                    .Append(owner)
                    .AppendPointer()
                    .ToString();
            }

            return new MetadataName(
                build: BuildPointerType,
                owner: owner)
            {
                IsType = true,
                IsPointerType = true
            };
        }

        public static MetadataName CreatePropertyName(MetadataName returnType, MetadataName owner, string name)
        {
            return CreateMethodName(owner, name, returnType);
        }

        public static MetadataName CreateTypeName(string @namespace, string name, int arity = 0)
        {
            ValidateName(name);
            ValidateArity(arity);

            string BuildTypeName(NamingConvention convention)
            {
                return new Builder(convention)
                    .Append(@namespace)
                    .AppendType(name)
                    .AppendArity(arity)
                    .ToString();
            }

            return new MetadataName(
                build: BuildTypeName,
                @namespace: @namespace,
                name: name,
                arity: arity)
            {
                IsType = true
            };
        }

        private static void ValidateArity(int arity)
        {
            if (arity < 0)
            {
                throw new InvalidMetadataNameException("Arity must not be negative.");
            }
        }

        private static void ValidateGenericArguments(ImmutableArray<MetadataName> genericArguments, MetadataName owner)
            => ValidateTypeArray(array: genericArguments,
                notTypeMessage: "All generic arguments of a generic instance type must be types.",
                expectedArity: owner.Arity,
                badArityMessage: "Expected {0} generic arguments, got {1}.");

        private static void ValidateGenericParameters(ImmutableArray<MetadataName> genericParameters, MetadataName owner)
            => ValidateTypeArray(array: genericParameters,
                notTypeMessage: "All generic parameters of a member must be types.");

        private static void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidMetadataNameException("Name must not be null, empty or white-space.");
            }
        }

        private static void ValidateOwner(MetadataName owner)
        {
            if (!owner.IsType)
            {
                throw new InvalidMetadataNameException("The owner of a nested type must be a type.");
            }
        }

        private static void ValidateParameters(ImmutableArray<MetadataName> parameters)
            => ValidateTypeArray(array: parameters,
                notTypeMessage: "All parameters of a member must be types.");

        private static void ValidateReturnType(MetadataName returnType)
        {
            if (!returnType.IsType)
            {
                throw new InvalidMetadataNameException("The return type of a member must be a type.");
            }
        }

        private static void ValidateTypeArray(ImmutableArray<MetadataName> array, string notTypeMessage, int expectedArity = -1,
            string badArityMessage = null)
        {
            if (expectedArity != -1 && array.Length != expectedArity)
            {
                throw new InvalidMetadataNameException(string.Format(badArityMessage, expectedArity, array.Length));
            }

            if(array.IsDefault || array.Length == 0)
            {
                return;
            }

            foreach (var item in array)
            {
                if (!item.IsType)
                {
                    throw new InvalidMetadataNameException(notTypeMessage);
                }
            }
        }
    }
}