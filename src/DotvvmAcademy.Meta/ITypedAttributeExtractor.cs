using Microsoft.CodeAnalysis;
using System;
using System.Collections.Immutable;

namespace DotvvmAcademy.Meta
{
    public interface ITypedAttributeExtractor : IAttributeExtractor
    {
        ImmutableArray<Attribute> Extract(Type attributeType, ISymbol symbol);

        ImmutableArray<AttributeData> ExtractRoslyn(Type attributeType, ISymbol symbol);

        bool HasAttribute(Type attributeType, ISymbol symbol);
    }
}