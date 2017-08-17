using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DotvvmAcademy.BL.Validation.CSharp
{
    public sealed class CSharpClassInstance : CSharpValidationObject<ClassDeclarationSyntax>
    {
        public CSharpClassInstance(CSharpValidate validate, ClassDeclarationSyntax node) : base(validate, node)
        {
        }

        public static CSharpClassInstance Inactive => new CSharpClassInstance(null, null) { IsActive = false };

        public void ExecuteMethod(string methodName, object expectedResult = null, params object[] arguments)
        {
            if (!IsActive) return;
        }

        public void PropertyValue(string propertyName, object expectedValue)
        {
            if (!IsActive) return;
        }
    }
}
