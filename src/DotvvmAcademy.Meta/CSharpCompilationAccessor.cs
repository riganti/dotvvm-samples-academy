using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Meta
{
    public class CSharpCompilationAccessor : ICSharpCompilationAccessor
    {
        public CSharpCompilation Compilation { get; set; }
    }
}