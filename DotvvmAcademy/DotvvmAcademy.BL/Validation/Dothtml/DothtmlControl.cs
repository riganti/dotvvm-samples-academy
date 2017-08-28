using DotVVM.Framework.Binding;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Tokenizer;
using DotVVM.Framework.Controls;
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

        public DothtmlProperty Attribute(string name)
        {
            if (!IsActive) return DothtmlProperty.Inactive;
            var attributeGroup = DotvvmPropertyGroup.GetPropertyGroups(typeof(HtmlGenericControl)).Single(g=>g.Name == "Attributes");
            var property = attributeGroup.GetDotvvmProperty(name);
            return Property(property);
        }

        public void Tag(string tagName, string tagPrefix = null)
        {
            if (!IsActive) return;

            if(!(Node.DothtmlNode is DothtmlElementNode element))
            {
                throw new DothtmlValidatorException("Tag cannot be validated on a non-element based Dothtml control.", this);
            }

            element = (DothtmlElementNode)Node.DothtmlNode;
            if(element.TagName != tagName)
            {
                AddError($"This element has an invalid tag name. Expected: '{tagName}'.",
                    element.TagNameNode.StartPosition, element.TagNameNode.EndPosition);
            }

            if(element.TagPrefix != null)
            {
                AddError($"This element has an invalid tag prefix. Expected: '{tagPrefix}'.",
                    element.TagPrefixNode.StartPosition, element.TagPrefixNode.EndPosition);
            }
        }

        public void Type<TControl>() where TControl : DotvvmControl
        {
            if (!IsActive) return;

            if(Node.Metadata.Type != typeof(TControl))
            {
                AddError($"This control should be of type '{typeof(TControl).Name}'");
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