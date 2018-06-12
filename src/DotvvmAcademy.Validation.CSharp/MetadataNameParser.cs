using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using static DotvvmAcademy.Validation.CSharp.MetadataNameFormatter;

namespace DotvvmAcademy.Validation.CSharp
{
    public class MetadataNameParser
    {
        private readonly char[] digits = new[]
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
        };

        private readonly IMetadataNameFactory factory;

        private readonly char[] specialChars = new[]
                        {
            ArityPrefix,
            ArrayEnd,
            ArrayStart,
            ListSeparator,
            MemberAccess,
            NamespaceAccess,
            NestedTypeAccess,
            ParameterListEnd,
            ParameterListStart,
            PointerSuffix,
            ReturnTypeSeparator,
            TypeArgumentListEnd,
            TypeArgumentListStart,
            TypeParameterAccess
        };

        private string @namespace;
        private int arity;
        private MetadataName current;
        private string input;
        private bool isContructedType;
        private bool isInRank;
        private bool isMember;
        private bool isMethod;
        private bool isNested;
        private List<MetadataName> list = new List<MetadataName>();
        private string name;
        private int position;
        private int rank;
        private MetadataName returnType;

        public MetadataNameParser(IMetadataNameFactory factory)
        {
            this.factory = factory;
        }

        public MetadataName Parse(string fullName)
        {
            Setup(fullName);
            while (position < input.Length)
            {
                switch (fullName[position])
                {
                    case ArityPrefix:
                        position++;
                        arity = ReadInteger();
                        continue;

                    case ArrayEnd:
                        break;

                    case ArrayStart:
                        current = CreateType();
                        isInRank = true;
                        break;

                    case ListSeparator:
                        if (isInRank)
                        {
                            rank++;
                        }
                        else if (isContructedType || isMethod)
                        {
                            list.Add(CreateType());
                        }
                        break;

                    case MemberAccess:
                        current = CreateType();
                        isMember = true;
                        break;

                    case NamespaceAccess:
                        if (@namespace.Length == 0)
                        {
                            @namespace = name;
                        }
                        else
                        {
                            @namespace = $"{@namespace}.{name}";
                        }
                        break;

                    case NestedTypeAccess:
                        current = CreateType();
                        isNested = true;
                        break;

                    case ParameterListStart:
                        isMethod = true;
                        break;

                    case PointerSuffix:
                        break;

                    case ReturnTypeSeparator:
                        returnType = CreateType();
                        break;

                    case TypeArgumentListEnd:
                        break;

                    case TypeArgumentListStart:
                        current = CreateType();
                        isContructedType = true;
                        break;

                    default:
                        name = ReadIdentifier();
                        continue;
                }
                position++;
            }
            if (isMember && isMethod)
            {
                return factory.CreateMethodName(current, name, returnType, arity, list.ToImmutableArray());
            }
            if (isMember)
            {
                return factory.CreateFieldName(current, name, returnType);
            }
            return CreateType();
        }

        private MetadataName CreateType()
        {
            MetadataName type;
            if (isNested)
            {
                type = factory.CreateNestedTypeName(current, name, arity);
                isNested = false;
                name = "";
                arity = 0;
                current = null;
            }
            else if (isInRank)
            {
                type = factory.CreateArrayTypeName(current, rank);
                isInRank = false;
                rank = 1;
                current = null;
            }
            else if (isContructedType)
            {
                type = factory.CreateConstructedTypeName(current, list.ToImmutableArray());
                isContructedType = false;
                list.Clear();
                current = null;
            }
            else
            {
                type = factory.CreateTypeName(@namespace, name, arity);
                @namespace = "";
                name = "";
                arity = 0;
            }
            return type;
        }

        private string ReadIdentifier()
        {
            int start = position;
            char letter = input[position];
            while (!specialChars.Contains(letter))
            {
                position++;
                if(position >= input.Length)
                {
                    break;
                }
                letter = input[position];
            }
            return input.Substring(start, position - start);
        }

        private int ReadInteger()
        {
            int start = position;
            char letter = input[position];
            while (!specialChars.Contains(letter) && digits.Contains(letter))
            {
                position++;
            }
            return int.Parse(input.Substring(start, position - start));
        }

        private void ReadRank()
        {
            char letter = input[position];
        }

        private void Setup(string fullName)
        {
            position = 0;
            input = fullName;
            @namespace = "";
            name = "";
            arity = 0;
            rank = 1;
            current = null;
            isInRank = false;
            isNested = false;
            isMember = false;
            list.Clear();
            isContructedType = false;
            returnType = null;
            isMethod = false;
        }
    }
}