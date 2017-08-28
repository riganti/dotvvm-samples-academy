using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace DotvvmAcademy.BL.Validation.CSharp
{
    public sealed class CSharpClass : CSharpObject<ClassDeclarationSyntax>
    {
        internal CSharpClass(CSharpValidate validate, ClassDeclarationSyntax node, bool isActive = true) : base(validate, node, isActive)
        {
            if (!IsActive) return;
            var model = Validate.Compilation.GetSemanticModel(Node.SyntaxTree);
            Symbol = model.GetDeclaredSymbol(Node);
        }

        public static CSharpClass Inactive => new CSharpClass(null, null, false);

        public INamedTypeSymbol Symbol { get; }

        public override void AddError(string message) => AddError(message, Node.Identifier.Span.Start, Node.Identifier.Span.End);

        public CSharpTypeDescriptor GetDescriptor(params CSharpTypeDescriptor[] genericParameters) 
            => Symbol.GetDescriptor(genericParameters);

        /// <param name="returnType">Null is considered as void.</param>
        public CSharpMethod Method(string name, CSharpTypeDescriptor returnType = null, params CSharpTypeDescriptor[] parameterTypes)
        {
            if (!IsActive || returnType?.IsActive == false || parameterTypes.Any(p => p?.IsActive == false)) return CSharpMethod.Inactive;

            returnType = returnType ?? Validate.Descriptor(typeof(void));

            var methodSignature = GetMethodSignature(name, returnType, parameterTypes);
            var missingErrorMessage = $"This class is missing the '{methodSignature}' method.";

            var methods = Node.Members
                .OfType<MethodDeclarationSyntax>()
                .Where(m => m.Identifier.ValueText == name)
                .ToList();
            if (methods.Count == 0)
            {
                AddError(missingErrorMessage);
                return CSharpMethod.Inactive;
            }

            var methodTuples = methods
                .Select(m => (Node: m, Symbol: Validate.Model.GetDeclaredSymbol(m)))
                .Where(t => t.Symbol.ReturnType.GetDescriptor().Equals(returnType))
                .ToList();
            if (methodTuples.Count == 0)
            {
                AddError(missingErrorMessage);
                return CSharpMethod.Inactive;
            }

            methodTuples = methodTuples
                .Where(t => ValidateParameterTypes(t.Symbol.Parameters, parameterTypes))
                .ToList();

            if (methodTuples.Count > 1)
            {
                AddError($"This class contains multiple '{methodSignature}' methods.");
                return CSharpMethod.Inactive;
            }

            if (methodTuples.Count == 0)
            {
                AddError(missingErrorMessage);
                return CSharpMethod.Inactive;
            }

            return new CSharpMethod(Validate, methodTuples.Single().Node);
        }

        public CSharpProperty Property(string name)
        {
            if (!IsActive) return CSharpProperty.Inactive;

            var properties = Node.Members.OfType<PropertyDeclarationSyntax>().Where(p => p.Identifier.ValueText == name).ToList();
            if (properties.Count > 1)
            {
                AddError($"This class should not contain multiple properties called '{name}'.");
                return CSharpProperty.Inactive;
            }

            if (properties.Count == 0)
            {
                AddError($"This class is missing the '{name}' property.");
                return CSharpProperty.Inactive;
            }

            return new CSharpProperty(Validate, properties.Single());
        }

        private string GetMethodSignature(string name, CSharpTypeDescriptor returnType, CSharpTypeDescriptor[] parameterTypes)
        {
            var sb = new StringBuilder();
            sb.Append($"{returnType.GetFriendlyName()} {name}(");
            for (int i = 0; i < parameterTypes.Length; i++)
            {
                sb.Append(parameterTypes[i].GetFriendlyName());
                if (i != parameterTypes.Length - 1)
                {
                    sb.Append(", ");
                }
            }
            sb.Append(')');
            return sb.ToString();
        }

        private bool ValidateParameterTypes(ImmutableArray<IParameterSymbol> parameterSymbols, CSharpTypeDescriptor[] parameterTypes)
        {
            if (parameterSymbols.Length != parameterTypes.Length)
            {
                return false;
            }

            for (int i = 0; i < parameterTypes.Length; i++)
            {
                var symbol = parameterSymbols[i];
                var descriptor = parameterTypes[i];
                if (!symbol.Type.GetDescriptor().Equals(descriptor))
                {
                    return false;
                }
            }

            return true;
        }
    }
}