using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Controls;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace DotvvmAcademy.BL.Validation.Dothtml
{
    public static class DothtmlControlCollectionExtensions
    {
        public static DothtmlControl Control<TControl>(this DothtmlControlCollection c) where TControl : DotvvmControl
        {
            if (!c.IsActive) return DothtmlControl.Inactive;

            var controls = c.GetControls<TControl>();

            if (controls.Count > 1)
            {
                c.AddError($"There should be only one control of type '{typeof(TControl).Name}'.");
                return DothtmlControl.Inactive;
            }

            if (controls.Count == 0)
            {
                c.AddError($"There should be a control of type '{typeof(TControl).Name}'.");
                return DothtmlControl.Inactive;
            }

            return controls.Single();
        }

        public static DothtmlControlCollection Controls<TControl>(this DothtmlControlCollection c) where TControl : DotvvmControl
        {
            if (!c.IsActive) return DothtmlControlCollection.Inactive;

            var controls = c.GetControls<TControl>();

            if (controls.Any())
            {
                c.Controls = controls;
                return c;
            }
            else
            {
                c.AddError($"There should be child controls of type '{typeof(TControl).Name}'.");
                return DothtmlControlCollection.Inactive;
            }
        }

        public static DothtmlControlCollection Controls<TControl>(this DothtmlControlCollection c, int count) where TControl : DotvvmControl
        {
            if (!c.IsActive) return DothtmlControlCollection.Inactive;

            var controls = c.GetControls<TControl>();

            if (controls.Count == count)
            {
                c.Controls = controls;
                return c;
            }
            else
            {
                c.AddError($"There should be {count} child controls of type '{typeof(TControl).Name}'.");
                return DothtmlControlCollection.Inactive;
            }
        }

        public static DothtmlControl Element(this DothtmlControlCollection c, string tagName, string tagPrefix = null)
        {
            if (!c.IsActive) return DothtmlControl.Inactive;

            var elements = c.GetElements(tagName, tagPrefix);
            if (elements.Count > 1)
            {
                c.AddError($"There should be only one child element with tag '{GetFullTag(tagName, tagPrefix)}'.");
                return DothtmlControl.Inactive;
            }

            if (elements.Count == 0)
            {
                c.AddError($"There should be a child element with tag '{GetFullTag(tagName, tagPrefix)}'.");
                return DothtmlControl.Inactive;
            }

            return elements.Single();
        }

        public static DothtmlControlCollection Elements(this DothtmlControlCollection c, string tagName, string tagPrefix = null)
        {
            if (!c.IsActive) return DothtmlControlCollection.Inactive;

            var elements = c.GetElements(tagName, tagPrefix);

            if (elements.Any())
            {
                c.Controls = elements;
                return c;
            }
            else
            {
                c.AddError($"This control should contain '{GetFullTag(tagName, tagPrefix)}' elements.");
                return DothtmlControlCollection.Inactive;
            }
        }

        public static DothtmlControlCollection Elements(this DothtmlControlCollection c, int? count = null)
        {
            if (!c.IsActive) return DothtmlControlCollection.Inactive;

            var elements = c.GetElements();
            if (count != null && elements.Count != count)
            {
                c.AddError($"There should be {count} elements.");
                return DothtmlControlCollection.Inactive;
            }

            c.Controls = elements;
            return c;
        }

        public static DothtmlControlCollection Elements(this DothtmlControlCollection c, int count, string tagName, string tagPrefix = null)
        {
            if (!c.IsActive) return DothtmlControlCollection.Inactive;

            var elements = c.GetElements(tagName, tagPrefix);
            if (elements.Count == count)
            {
                c.Controls = elements;
                return c;
            }
            else
            {
                c.AddError($"There should be {count} child elements with tag '{GetFullTag(tagName, tagPrefix)}'.");
                return DothtmlControlCollection.Inactive;
            }
        }

        private static ImmutableList<DothtmlControl> GetControls<TControl>(this DothtmlControlCollection c)
            where TControl : DotvvmControl
        {
            return c.Controls
                .Where(d => d.Node.Metadata.Type == typeof(TControl))
                .ToImmutableList();
        }

        private static ImmutableList<DothtmlControl> GetElements(this DothtmlControlCollection c, string tagName, string tagPrefix = null)
        {
            return c.Controls.Where(control =>
            {
                if (control.Node.DothtmlNode is DothtmlElementNode node)
                {
                    return node.TagName == tagName && node.TagPrefix == tagPrefix;
                }
                return false;
            }).ToImmutableList();
        }

        private static ImmutableList<DothtmlControl> GetElements(this DothtmlControlCollection c)
        {
            return c.Controls.Where(control => control.Node.DothtmlNode is DothtmlElementNode)
                .ToImmutableList();
        }

        private static string GetFullTag(string tagName, string tagPrefix = null)
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