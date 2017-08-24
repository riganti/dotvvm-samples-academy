using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotvvmAcademy.BL.Validation.Dothtml
{
    public abstract class DothtmlObject<TNode> : ValidationObject<DothtmlValidate>
        where TNode : ResolvedContentNode
    {
        internal DothtmlObject(DothtmlValidate validate, TNode node, bool isActive = true) : base(validate, isActive)
        {
            Node = node;
        }

        public TNode Node { get; }

        public DothtmlControl Control<TControl>() where TControl : DotvvmControl
        {
            if (!IsActive) return DothtmlControl.Inactive;

            var controls = Controls<TControl>();
            if (!controls.IsActive) return DothtmlControl.Inactive;

            if (controls.Count() > 1)
            {
                AddError($"There should be only one control of type '{typeof(TControl).Name}'.");
                return DothtmlControl.Inactive;
            }

            if (controls.Count() == 0)
            {
                AddError($"There should be a control of type '{typeof(TControl).Name}'.");
                return DothtmlControl.Inactive;
            }

            return controls.Single();
        }

        public DothtmlControlCollection Controls<TControl>() where TControl : DotvvmControl
        {
            if (!IsActive) return DothtmlControlCollection.Inactive;

            var controls = Children();
            controls.Controls = controls.Controls
                .Where(c => c.Node.Metadata.Type == typeof(TControl))
                .ToList();

            if (controls.Any())
            {
                return controls;
            }
            else
            {
                AddError($"There should be child controls of type '{typeof(TControl).Name}'.");
                return DothtmlControlCollection.Inactive;
            }
        }

        public DothtmlControlCollection Controls<TControl>(int count) where TControl : DotvvmControl
        {
            if (!IsActive) return DothtmlControlCollection.Inactive;

            var controls = Controls<TControl>();
            if(!controls.IsActive) return controls;

            if (controls.Count() == count)
            {
                return controls;
            }
            else
            {
                AddError($"There should be {count} child controls of type '{typeof(TControl).Name}'.");
                return DothtmlControlCollection.Inactive;
            }
        }

        public DothtmlControl Element(string tagName, string tagPrefix = null)
        {
            if (!IsActive) return DothtmlControl.Inactive;

            var elements = Elements(tagName, tagPrefix).Controls.ToList();
            if (elements.Count > 1)
            {
                AddError($"There should be only one child element with tag '{GetFullTag(tagName, tagPrefix)}'.");
                return DothtmlControl.Inactive;
            }

            if (elements.Count == 0)
            {
                AddError($"There should be a child element with tag '{GetFullTag(tagName, tagPrefix)}'.");
                return DothtmlControl.Inactive;
            }

            return elements.Single();
        }

        public DothtmlControlCollection Elements(string tagName, string tagPrefix = null)
        {
            if (!IsActive) return DothtmlControlCollection.Inactive;

            var collection = Children();
            collection.Controls = collection.Controls.Where(e =>
            {
                var node = (DothtmlElementNode)e.Node.DothtmlNode;
                return node.TagName == tagName && node.TagPrefix == tagPrefix;
            }).ToList();

            if (collection.Any())
            {
                return collection;
            }
            else
            {
                AddError($"This control should contain '{GetFullTag(tagName, tagPrefix)}' elements.");
                return DothtmlControlCollection.Inactive;
            }
        }

        public DothtmlControlCollection Elements(int count, string tagName, string tagPrefix = null)
        {
            if (!IsActive) return DothtmlControlCollection.Inactive;

            var elements = Elements(tagName, tagPrefix);
            if (elements.Count() == count)
            {
                return elements;
            }
            else
            {
                AddError($"There should be {count} child elements with tag '{GetFullTag(tagName, tagPrefix)}'.");
                return DothtmlControlCollection.Inactive;
            }
        }

        public DothtmlControlCollection Children(int count)
        {
            if (!IsActive) return DothtmlControlCollection.Inactive;

            var elements = Children();
            var actualCount = elements.Count();
            if (actualCount != count)
            {
                AddError($"This element should have {count} children.");
                return DothtmlControlCollection.Inactive;
            }
            return elements;
        }

        public DothtmlControlCollection Children()
        {
            if (!IsActive) return DothtmlControlCollection.Inactive;

            return new DothtmlControlCollection(Validate,
                Node.Content
                .Where(c => c.DothtmlNode is DothtmlElementNode)
                .Select(c => new DothtmlControl(Validate, c)));
        }

        protected override void AddError(string message) => AddError(message, Node.DothtmlNode.StartPosition, Node.DothtmlNode.EndPosition);

        private string GetFullTag(string tagName, string tagPrefix = null)
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(tagPrefix))
            {
                sb.Append(tagPrefix);
                sb.Append(":");
            }
            sb.Append(tagName);
            return sb.ToString();
        }
    }
}