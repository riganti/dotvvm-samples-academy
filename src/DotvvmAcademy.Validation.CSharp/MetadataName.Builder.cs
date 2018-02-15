using System.Collections.Immutable;
using System.Text;

namespace DotvvmAcademy.Validation.CSharp
{
    public sealed partial class MetadataName
    {
        public sealed class Builder
        {
            private readonly NamingConvention convention;
            private readonly StringBuilder sb = new StringBuilder();

            public Builder(NamingConvention convention)
            {
                this.convention = convention;
            }

            public Builder Append(string value)
            {
                sb.Append(value);
                return this;
            }

            public Builder Append(MetadataName name)
            {
                sb.Append(name.build(convention));
                return this;
            }

            public Builder AppendArity(int arity)
            {
                if (arity != 0)
                {
                    sb.Append(convention.ArityPrefix)
                        .Append(arity);
                }
                return this;
            }

            public Builder AppendArraySyntax()
            {
                sb.Append(convention.ArrayStart)
                    .Append(convention.ArrayEnd);
                return this;
            }

            public Builder AppendGeneric(ImmutableArray<MetadataName> array)
            {
                AppendList(
                    prefix: convention.GenericParameterListPrefix,
                    separator: convention.GenericParameterSeparator,
                    suffix: convention.GenericParameterListSuffix,
                    values: array,
                    allowEmpty: false);
                return this;
            }

            public Builder AppendMember(string memberName)
            {
                if (sb.Length != 0)
                {
                    sb.Append(convention.MemberAccessOperator);
                }

                sb.Append(memberName);
                return this;
            }

            public Builder AppendNestedType(string nestedTypeName)
            {
                sb.Append(convention.NestedTypeAccessOperator)
                    .Append(nestedTypeName);
                return this;
            }

            public Builder AppendParameters(ImmutableArray<MetadataName> parameters)
            {
                AppendList(
                    prefix: convention.ParameterListPrefix,
                    separator: convention.ParameterSeparator,
                    suffix: convention.ParameterListSuffix,
                    values: parameters,
                    allowEmpty: true);
                return this;
            }

            public Builder AppendPointer()
            {
                sb.Append(convention.PointerSyntax);
                return this;
            }

            public Builder AppendReturnType(MetadataName returnType)
            {
                Append(returnType);
                sb.Append(convention.ReturnTypeSuffix);
                return this;
            }

            public Builder AppendType(string typeName)
            {
                if (sb.Length != 0)
                {
                    sb.Append(convention.TypeAccessOperator);
                }

                sb.Append(typeName);
                return this;
            }

            public override string ToString()
            {
                return sb.ToString();
            }

            private void AppendList(string prefix, string separator, string suffix, ImmutableArray<MetadataName> values, bool allowEmpty)
            {
                var isEmpty = values.IsDefault || values.Length == 0;
                if (!allowEmpty && isEmpty)
                {
                    return;
                }

                sb.Append(prefix);
                if (!isEmpty)
                {
                    sb.Append(values[0].build(convention));
                    for (int i = 1; i < values.Length; i++)
                    {
                        sb.Append(separator)
                            .Append(values[i].build(convention));
                    }
                }

                sb.Append(suffix);
            }
        }
    }
}