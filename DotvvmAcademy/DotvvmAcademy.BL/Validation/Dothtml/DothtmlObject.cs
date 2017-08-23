using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Controls;
using System.Linq;

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

            var controls = GetControls<TControl>().ToList();
            if (controls.Count > 1)
            {
                AddError($"This control should contain only one control of type '{typeof(TControl).Name}'.");
                return DothtmlControl.Inactive;
            }

            if (controls.Count == 0)
            {
                AddError($"This control should contain a control of type '{typeof(TControl).Name}'.");
                return DothtmlControl.Inactive;
            }

            return controls.Single();
        }

        public DothtmlControlCollection Controls<TControl>() where TControl : DotvvmControl
        {
            if (!IsActive) return DothtmlControlCollection.Inactive;

            var controls = GetControls<TControl>();
            if (controls.Any())
            {
                return controls;
            }
            else
            {
                AddError($"This control doesn't contain any controls of type '{typeof(TControl).Name}'.");
                return DothtmlControlCollection.Inactive;
            }
        }

        public DothtmlControlCollection Controls<TControl>(int count) where TControl : DotvvmControl
        {
            if (!IsActive) return DothtmlControlCollection.Inactive;

            var controls = GetControls<TControl>();
            if (controls.Count() == count)
            {
                return controls;
            }
            else
            {
                AddError($"This control should contain {count} controls of type '{typeof(TControl).Name}'.");
                return DothtmlControlCollection.Inactive;
            }
        }

        public DothtmlControl Element(string elementTag)
        {
            if (!IsActive) return DothtmlControl.Inactive;

            var elements = GetElements(elementTag).ToList();
            if (elements.Count > 1)
            {
                AddError($"This control should contain only one element with tag '{elementTag}'.");
                return DothtmlControl.Inactive;
            }

            if (elements.Count == 0)
            {
                AddError($"This control should contain an element with tag '{elementTag}'.");
                return DothtmlControl.Inactive;
            }

            return elements.Single();
        }

        public DothtmlControlCollection Elements(string elementTag)
        {
            if (!IsActive) return DothtmlControlCollection.Inactive;

            var elements = GetElements(elementTag);
            if (elements.Any())
            {
                return elements;
            }
            else
            {
                AddError($"This control doesn't contain any elements with tag '{elementTag}'.");
                return DothtmlControlCollection.Inactive;
            }
        }

        public DothtmlControlCollection Elements(string elementTag, int count)
        {
            if (!IsActive) return DothtmlControlCollection.Inactive;

            var elements = GetElements(elementTag);
            if (elements.Count() == count)
            {
                return elements;
            }
            else
            {
                AddError($"This control should contain {count} elements with tag '{elementTag}'.");
                return DothtmlControlCollection.Inactive;
            }
        }

        protected override void AddError(string message) => AddError(message, Node.DothtmlNode.StartPosition, Node.DothtmlNode.EndPosition);

        protected DothtmlControlCollection GetControls<TControl>() where TControl : DotvvmControl
        {
            return new DothtmlControlCollection(Validate,
                Node.Content
                .Where(c => c.Metadata.Type == typeof(TControl))
                .Select(c => new DothtmlControl(Validate, c)));
        }

        protected DothtmlControlCollection GetElements(string elementTag)
        {
            return new DothtmlControlCollection(Validate,
                Node.Content
                .Where(c => c.DothtmlNode is DothtmlElementNode)
                .Where(c => ((DothtmlElementNode)c.DothtmlNode).TagName == elementTag)
                .Select(c => new DothtmlControl(Validate, c)));
        }
    }
}