using Microsoft.CodeAnalysis;
using System;
using System.Linq;

namespace DotvvmAcademy.Meta
{
    public class TypedConstantExtractor : ITypedConstantExtractor
    {
        private readonly MetaConverter converter;

        public TypedConstantExtractor(MetaConverter converter)
        {
            this.converter = converter;
        }

        public object Extract(TypedConstant constant)
        {
            if (constant.IsNull)
            {
                return null;
            }

            switch (constant.Kind)
            {
                case TypedConstantKind.Enum:
                case TypedConstantKind.Primitive:
                    return constant.Value;

                case TypedConstantKind.Type:
                    return (Type)converter.ToReflectionSingle((ITypeSymbol)constant.Value);

                case TypedConstantKind.Array:
                    var elementType = (Type)converter.ToReflectionSingle(((IArrayTypeSymbol)constant.Type).ElementType);
                    var values = Array.CreateInstance(elementType, constant.Values.Length);
                    for (var i = 0; i < constant.Values.Length; ++i)
                    {
                        values.SetValue(Extract(constant.Values[i]), i);
                    }
                    return values;

                default:
                    throw new ArgumentException($"Constant '{constant}' could not be extracted.", nameof(constant));
            }
        }
    }
}