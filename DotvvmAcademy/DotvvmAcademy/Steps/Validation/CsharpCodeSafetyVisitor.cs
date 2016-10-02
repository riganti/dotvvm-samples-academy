using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;

namespace DotvvmAcademy.Steps.Validation
{
    public class CSharpCodeSafetyVisitor : CSharpSyntaxWalker
    {

        private CodeStep step;
        private CSharpCompilation compilation;
        private CSharpSyntaxTree tree;
        private SemanticModel model;

        public CSharpCodeSafetyVisitor(CodeStep step, CSharpCompilation compilation, CSharpSyntaxTree tree, SemanticModel model)
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
