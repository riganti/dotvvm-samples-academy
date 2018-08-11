using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotvvmAcademy.Meta
{
    public interface ICSharpCompilationAccessor
    {
        CSharpCompilation Compilation { get; set; }
    }
}