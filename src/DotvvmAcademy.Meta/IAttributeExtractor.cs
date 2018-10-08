using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Meta
{
    public interface IAttributeExtractor
    {
        Attribute Extract(AttributeData attributeData);

        IEnumerable<Attribute> Extract(Type attributeType, ISymbol symbol);

        IEnumerable<AttributeData> ExtractAttributeData(Type attributeType, ISymbol symbol);

        bool HasAttribute(Type attributeType, ISymbol symbol);
    }
}