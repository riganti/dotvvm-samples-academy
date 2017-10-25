using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.Validation.CSharp.Analyzers
{
    public abstract class ValidationAnalyzer : DiagnosticAnalyzer
    {


        public CSharpValidationContext Context { get; set; }
    }
}
