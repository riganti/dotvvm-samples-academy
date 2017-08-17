using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DotvvmAcademy.BL.Validation.CSharp
{
    public sealed class CSharpClass : CSharpValidationObject<ClassDeclarationSyntax>
    {
        public CSharpClass(CSharpValidate validate, ClassDeclarationSyntax node) : base(validate, node)
        {
        }

        public static CSharpClass Inactive => new CSharpClass(null, null) { IsActive = false };

        public CSharpProperty Property<TProperty>(string name)
        {
            if (!IsActive) return CSharpProperty.Inactive;
            return CSharpProperty.Inactive;
        }

        public void Method(string name, Type returnType, params Type[] parameters)
        {
            if (!IsActive) return;
        }

        public CSharpClassInstance Instance()
        {
            if (!IsActive) return CSharpClassInstance.Inactive;
            return CSharpClassInstance.Inactive;
        }
    }
}