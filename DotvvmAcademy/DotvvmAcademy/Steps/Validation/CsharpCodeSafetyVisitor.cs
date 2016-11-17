using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DotvvmAcademy.Steps.Validation
{
    public class CSharpCodeSafetyVisitor : CSharpSyntaxWalker
    {
        private CSharpCompilation compilation;
        private readonly SemanticModel model;

        private readonly CodeStepCsharp step;
        private CSharpSyntaxTree tree;

        public CSharpCodeSafetyVisitor(CodeStepCsharp step, CSharpCompilation compilation, CSharpSyntaxTree tree,
            SemanticModel model)
        {
            this.step = step;
            this.compilation = compilation;
            this.tree = tree;
            this.model = model;
        }


        public override void VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            var symbol = model.GetSymbolInfo(node).Symbol;
            var fullMethodName = symbol.ToDisplayString();
            if (fullMethodName.Contains("("))
            {
                fullMethodName = fullMethodName.Substring(0, fullMethodName.IndexOf("("));
            }

            if (!step.AllowedMethodsCalled.Contains(fullMethodName))
            {
                throw new CodeValidationException(string.Format(Texts.IllegalMethodCall, fullMethodName));
            }

            base.VisitInvocationExpression(node);
        }

        public override void VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
        {
            var symbol = model.GetSymbolInfo(node).Symbol.ContainingType;
            var fullTypeName = symbol.ToDisplayString();

            if (!step.AllowedTypesConstructed.Contains(fullTypeName))
            {
                throw new CodeValidationException(string.Format(Texts.IllegalConstructorCall, fullTypeName));
            }

            base.VisitObjectCreationExpression(node);
        }
    }
}