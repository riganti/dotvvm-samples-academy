using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Tokenizer;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotvvmAcademy.BL.Validation.Dothtml
{
    public sealed class DothtmlControl : DothtmlObject<ResolvedControl>
    {
        internal DothtmlControl(DothtmlValidate validate, ResolvedControl node, bool isActive = true) : base(validate, node, isActive)
        {
        }

        public static DothtmlControl Inactive => new DothtmlControl(null, null, false);

        public void Attribute(string name, string expectedValue)
        {
            if (!IsActive) return;

            var node = (DothtmlElementNode)Node.DothtmlNode;
            var attribute = node.Attributes.SingleOrDefault(a => a.AttributeName == name);

            if (attribute == null)
            {
                AddError($"This control should have the '{name}' attribute set.");
                return;
            }

            if (TokensToString(attribute.ValueNode.Tokens) != expectedValue)
            {
                AddError($"The '{name}' attribute contains incorrect value.", attribute.AttributeNameNode.StartPosition, attribute.AttributeNameNode.EndPosition);
            }
        }

        public DothtmlProperty Property(IPropertyDescriptor property)
        {
            if (!IsActive) return DothtmlProperty.Inactive;

            if (!Node.TryGetProperty(property, out var setter))
            {
                AddError($"The '{property.Name}' property should be set.");
                return DothtmlProperty.Inactive;
            }

            return new DothtmlProperty(Validate, setter);
        }

        private string TokensToString(IEnumerable<DothtmlToken> tokens)
        {
            var builder = new StringBuilder();
            foreach (var token in tokens)
            {
                builder.Append(token.Text);
            }
            return builder.ToString();
        }
    }
}