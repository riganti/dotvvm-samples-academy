using Microsoft.CodeAnalysis;
using System;
using System.Linq;
using System.Reflection;

namespace DotvvmAcademy.Meta
{
    public class TypedConstantExtractor : ITypedConstantExtractor
    {
        private readonly IMetaConverter<ISymbol, MemberInfo> symbolConverter;

        public TypedConstantExtractor(IMetaConverter<ISymbol, MemberInfo> symbolConverter)
        {
            this.symbolConverter = symbolConverter;
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
                    return (Type)symbolConverter
                        .Convert((ITypeSymbol)constant.Value)
                        .Single();

                case TypedConstantKind.Array:
                    var elementType = (Type)symbolConverter
                        .Convert(((IArrayTypeSymbol)constant.Type).ElementType)
                        .Single();
                    var objects = constant.Values.Select(Extract).ToArray();
                    var values = Array.CreateInstance(elementType, objects.Length);
                    for (int i = 0; i < values.Length; i++)
                    {
                        values.SetValue(objects[i], i);
                    }
                    return values;

                default:
                    throw new ArgumentException($"Constant '{constant}' could not be extracted.", nameof(constant));
            }
        }
    }
}