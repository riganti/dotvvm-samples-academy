using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DotvvmAcademy.BL.Validation.CSharp
{
    public sealed class CSharpClassInstance : CSharpValidationObject<ClassDeclarationSyntax>
    {
        internal CSharpClassInstance(CSharpValidate validate, ClassDeclarationSyntax node, bool isActive = true) : base(validate, node, isActive)
        {
        }

        public static CSharpClassInstance Inactive => new CSharpClassInstance(null, null, false);

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
