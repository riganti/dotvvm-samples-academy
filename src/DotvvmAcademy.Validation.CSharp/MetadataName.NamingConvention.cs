using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp
{
    public sealed partial class MetadataName
    {
        public static readonly NamingConvention CecilConvention = new NamingConvention(
            memberOptions: NamingConventionMemberOptions.IncludeReturnType
                | NamingConventionMemberOptions.IncludeOwner
                | NamingConventionMemberOptions.IncludeParameters);

        public static readonly NamingConvention DefaultConvention = new NamingConvention();

        public static readonly NamingConvention ReflectionConvention = new NamingConvention(
            genericParameterListPrefix: "[",
            genericParameterListSuffix: "]",
            memberAccessOperator: ".",
            nestedTypeAccessOperator: "+",
            memberOptions: NamingConventionMemberOptions.None);

        [Flags]
        public enum NamingConventionMemberOptions
        {
            None = 0,
            IncludeReturnType = 1 << 0,
            IncludeOwner = 1 << 1,
            IncludeGenericParameters = 1 << 2,
            IncludeParameters = 1 << 3
        }

        public sealed class NamingConvention : IEquatable<NamingConvention>
        {
            public NamingConvention(
                string arityPrefix = "`",
                string arrayEnd = "]",
                string arrayStart = "[",
                string genericParameterListPrefix = "<",
                string genericParameterListSuffix = ">",
                string genericParameterSeparator = ",",
                string memberAccessOperator = "::",
                string nestedTypeAccessOperator = "/",
                string parameterListPrefix = "(",
                string parameterListSuffix = ")",
                string parameterSeparator = ",",
                string pointerSyntax = "*",
                string returnTypeSuffix = " ",
                string typeAccessOperator = ".",
                NamingConventionMemberOptions memberOptions = NamingConventionMemberOptions.IncludeReturnType
                    | NamingConventionMemberOptions.IncludeOwner
                    | NamingConventionMemberOptions.IncludeGenericParameters
                    | NamingConventionMemberOptions.IncludeParameters)
            {
                ArityPrefix = arityPrefix;
                ArrayEnd = arrayEnd;
                ArrayStart = arrayStart;
                GenericParameterListPrefix = genericParameterListPrefix;
                GenericParameterListSuffix = genericParameterListSuffix;
                GenericParameterSeparator = genericParameterSeparator;
                MemberAccessOperator = memberAccessOperator;
                NestedTypeAccessOperator = nestedTypeAccessOperator;
                ParameterListPrefix = parameterListPrefix;
                ParameterListSuffix = parameterListSuffix;
                ParameterSeparator = parameterSeparator;
                PointerSyntax = pointerSyntax;
                ReturnTypeSuffix = returnTypeSuffix;
                TypeAccessOperator = typeAccessOperator;
                MemberOptions = memberOptions;
            }

            public string ArityPrefix { get; }

            public string ArrayEnd { get; }

            public string ArrayStart { get; }

            public string GenericParameterListPrefix { get; }

            public string GenericParameterListSuffix { get; }

            public string GenericParameterSeparator { get; }

            public string MemberAccessOperator { get; }

            public NamingConventionMemberOptions MemberOptions { get; }

            public string NestedTypeAccessOperator { get; }

            public string ParameterListPrefix { get; }

            public string ParameterListSuffix { get; }

            public string ParameterSeparator { get; }

            public string PointerSyntax { get; }

            public string ReturnTypeSuffix { get; }

            public string TypeAccessOperator { get; }

            public static bool operator !=(NamingConvention convention1, NamingConvention convention2)
            {
                return !(convention1 == convention2);
            }

            public static bool operator ==(NamingConvention convention1, NamingConvention convention2)
            {
                return EqualityComparer<NamingConvention>.Default.Equals(convention1, convention2);
            }

            public override bool Equals(object obj)
            {
                return Equals(obj as NamingConvention);
            }

            public bool Equals(NamingConvention other)
            {
                return other != null &&
                       ArityPrefix == other.ArityPrefix &&
                       ArrayEnd == other.ArrayEnd &&
                       ArrayStart == other.ArrayStart &&
                       GenericParameterListPrefix == other.GenericParameterListPrefix &&
                       GenericParameterListSuffix == other.GenericParameterListSuffix &&
                       GenericParameterSeparator == other.GenericParameterSeparator &&
                       MemberAccessOperator == other.MemberAccessOperator &&
                       MemberOptions == other.MemberOptions &&
                       NestedTypeAccessOperator == other.NestedTypeAccessOperator &&
                       ParameterListPrefix == other.ParameterListPrefix &&
                       ParameterListSuffix == other.ParameterListSuffix &&
                       ParameterSeparator == other.ParameterSeparator &&
                       ReturnTypeSuffix == other.ReturnTypeSuffix &&
                       TypeAccessOperator == other.TypeAccessOperator;
            }

            public override int GetHashCode()
            {
                var hashCode = 185036485;
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ArityPrefix);
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ArrayEnd);
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ArrayStart);
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(GenericParameterListPrefix);
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(GenericParameterListSuffix);
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(GenericParameterSeparator);
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(MemberAccessOperator);
                hashCode = hashCode * -1521134295 + MemberOptions.GetHashCode();
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(NestedTypeAccessOperator);
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ParameterListPrefix);
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ParameterListSuffix);
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ParameterSeparator);
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ReturnTypeSuffix);
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TypeAccessOperator);
                return hashCode;
            }
        }
    }
}