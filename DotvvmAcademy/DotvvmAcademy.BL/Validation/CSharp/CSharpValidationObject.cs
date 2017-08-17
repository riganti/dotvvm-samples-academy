using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.BL.Validation.CSharp
{
    public abstract class CSharpValidationObject<TNode> where TNode : CSharpSyntaxNode
    {
        public CSharpValidationObject(CSharpValidate validate, TNode node)
        {
            Validate = validate;
            Node = node;
        }

        public bool IsActive { get; protected set; } = true;

        protected TNode Node { get; }

        protected CSharpValidate Validate { get; }

        protected void AddError(string message)
        {
            var location = Node.GetLocation();
            Validate.AddError(message, location.SourceSpan.Start, location.SourceSpan.End);
        }

        protected void AddError(string message, int startPosition, int endPosition)
        {
            Validate.AddError(message, startPosition, endPosition);
        }
    }
}