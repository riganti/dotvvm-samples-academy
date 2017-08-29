using DotVVM.Framework.Compilation.ControlTree.Resolved;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Validation.Dothtml
{
    public abstract class DothtmlObject<TNode> : ValidationObject<DothtmlValidate>, IDothtmlObject<TNode>
        where TNode : ResolvedContentNode
    {
        internal DothtmlObject(DothtmlValidate validate, TNode node, bool isActive = true) : base(validate, isActive)
        {
            Node = node;
        }

        public TNode Node { get; }

        public override void AddError(string message) => AddError(message, Node.DothtmlNode.StartPosition, Node.DothtmlNode.EndPosition);

        public DothtmlControlCollection Children(int count)
        {
            if (!IsActive) return DothtmlControlCollection.Inactive;

            var children = Children();
            if (children.Controls.Count != count)
            {
                AddError($"This element should have {count} children.");
                return DothtmlControlCollection.Inactive;
            }
            return children;
        }

        public DothtmlControlCollection Children()
        {
            if (!IsActive) return DothtmlControlCollection.Inactive;

            return new DothtmlControlCollection(Validate, Node.Content
                .Select(c => new DothtmlControl(Validate, c))
                .ToImmutableList(), Node);
        }
    }
}