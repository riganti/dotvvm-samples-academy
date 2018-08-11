using Microsoft.CodeAnalysis;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotvvmAcademy.Meta
{
    public interface ITypedConstantExtractor
    {
        object Extract(TypedConstant constant);
    }
}