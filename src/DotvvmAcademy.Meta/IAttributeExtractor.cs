using Microsoft.CodeAnalysis;
using System;

namespace DotvvmAcademy.Meta
{
    public interface IAttributeExtractor
    {
        Attribute Extract(AttributeData attributeData);
    }
}