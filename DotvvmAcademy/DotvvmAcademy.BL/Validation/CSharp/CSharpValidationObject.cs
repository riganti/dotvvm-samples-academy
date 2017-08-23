using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.BL.Validation.CSharp
{
    public abstract class CSharpValidationObject<TNode> where TNode : CSharpSyntaxNode
    {
        internal CSharpValidationObject(CSharpValidate validate, TNode node, bool isActive = true)
        {
            Validate = validate;
            Node = node;
            IsActive = isActive;
        }

        public bool IsActive { get; }

        protected TNode Node { get; }

        protected CSharpValidate Validate { get; }

        protected void AddError(string message, int startPosition, int endPosition) => Validate.AddError(message, startPosition, endPosition);

        protected void AddGlobalError(string message) => Validate.AddGlobalError(message);
    }
}