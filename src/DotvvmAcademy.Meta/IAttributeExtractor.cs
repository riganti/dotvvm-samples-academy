using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotvvmAcademy.Meta
{
    public interface IAttributeExtractor
    {
        Attribute Extract(AttributeData attributeData);
    }
}
