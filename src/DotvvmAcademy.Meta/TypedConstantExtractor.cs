using Microsoft.CodeAnalysis;
using System;
using System.Linq;

namespace DotvvmAcademy.Meta
{
    public class TypedConstantExtractor : ITypedConstantExtractor
    {
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
                    return Type.GetType(FullNamer.FromRoslyn((ITypeSymbol)constant.Value, Qualification.Assembly));

                case TypedConstantKind.Array:
                    return constant.Values.Select(Extract).ToArray();

                default:
                    throw new ArgumentException($"Constant '{constant}' could not be extracted.", nameof(constant));
            }
        }
    }
}