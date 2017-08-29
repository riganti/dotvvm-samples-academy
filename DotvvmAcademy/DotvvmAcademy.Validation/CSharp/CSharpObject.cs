using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Validation.CSharp
{
    public abstract class CSharpObject<TNode> : ValidationObject<CSharpValidate>
        where TNode : CSharpSyntaxNode
    {
        internal CSharpObject(CSharpValidate validate, TNode node, bool isActive = true) : base(validate, isActive)
        {
            Node = node;
        }

        public TNode Node { get; }
    }
}