using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace DotvvmAcademy.BL.Validation.CSharp
{
    public sealed class CSharpClassInstance : CSharpObject<ClassDeclarationSyntax>
    {
        internal CSharpClassInstance(CSharpValidate validate, ClassDeclarationSyntax node, object instance, bool isActive = true) : base(validate, node, isActive)
        {
            if (!isActive) return;
            Instance = instance;
        }

        public static CSharpClassInstance Inactive => new CSharpClassInstance(null, null, null, false);

        public object Instance { get; }

        public void MethodExecution(CSharpMethod method, object expectedResult = null, params object[] arguments)
        {
            if (!IsActive || !method.IsActive) return;

            var methodInfo = Instance.GetType().GetMethod(method.Symbol.Name);
            var result = methodInfo.Invoke(Instance, arguments);
            if(result != expectedResult)
            {
                AddError($"The '{method.Symbol.Name}' method produced an unexpected result: '{result}'. Expected: '{expectedResult}'.");
            }
        }

        public void PropertyGetter(CSharpProperty property, object expectedValue)
        {
            if (!IsActive || !property.IsActive) return;

            var propertyInfo = Instance.GetType().GetProperty(property.Symbol.Name);
            var value = propertyInfo.GetValue(Instance);
            if (!value.Equals(expectedValue))
            {
                AddError($"The '{property.Symbol.Name}' property contains " +
                    $"an unexpected value: '{value}'. Expected: '{expectedValue}'.", 
                    property.Node.Identifier.Span.Start,
                    property.Node.Identifier.Span.End);
            }
        }

        public void PropertySetter(CSharpProperty property, object value)
        {
            if (!IsActive || !property.IsActive) return;

            var propertyInfo = Instance.GetType().GetProperty(property.Symbol.Name);
            propertyInfo.SetValue(Instance, value);
        }

        protected override void AddError(string message) => AddError(message, Node.Identifier.Span.Start, Node.Identifier.Span.End);

    }
}
