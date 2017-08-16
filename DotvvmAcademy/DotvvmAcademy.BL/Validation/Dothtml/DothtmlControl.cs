using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Tokenizer;
using DotVVM.Framework.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DotvvmAcademy.BL.Validation.Dothtml
{
    public sealed class DothtmlControl
    {
        private ResolvedContentNode control;
        private DothtmlValidate validate;

        public DothtmlControl(ResolvedContentNode control, DothtmlValidate validate)
        {
            this.control = control;
            this.validate = validate;
        }

        public static DothtmlControl Inactive => new DothtmlControl(null, null) { IsActive = false };

        public bool IsActive { get; private set; } = true;

        public void AttributeValue(string name, string expectedValue)
        {
            if (!IsActive) return;

            var node = (DothtmlElementNode)control.DothtmlNode;
            var attribute = node.Attributes.SingleOrDefault(a => a.AttributeName == name);

            if (attribute == null)
            {
                AddError($"This control should have the {name} attribute set.");
                return;
            }

            if (TokensToString(attribute.ValueNode.Tokens) != expectedValue)
            {
                AddError($"The {name} attribute contains incorrect value.", attribute.AttributeNameNode.StartPosition, attribute.AttributeNameNode.EndPosition);
            }
        }

        //public void ValueBinding(IPropertyDescriptor property, Expression<Func<object>> valueAccessor)
        //{

        //}

        public DothtmlControlCollection Controls<TControl>() where TControl : DotvvmControl
        {
            return Controls(typeof(TControl).Name);
        }

        public DothtmlControlCollection Controls(string controlType)
        {
            if (!IsActive) return DothtmlControlCollection.Inactive;

            var controls = GetControls(controlType);
            if (controls.Any())
            {
                return controls;
            }
            else
            {
                AddError($"The control doesn't contain any controls of type {controlType}.");
                return DothtmlControlCollection.Inactive;
            }
        }

        public DothtmlControlCollection Controls<TControl>(int count) where TControl : DotvvmControl
        {
            return Controls(typeof(TControl).Name, count);
        }

        public DothtmlControlCollection Controls(string controlType, int count)
        {
            if (!IsActive) return DothtmlControlCollection.Inactive;

            var controls = GetControls(controlType);
            if (controls.Count() == count)
            {
                return controls;
            }
            else
            {
                AddError($"The control should contain {count} controls of type {controlType}.");
                return DothtmlControlCollection.Inactive;
            }
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
                AddError($"The control doesn't contain any elements with tag {elementTag}.");
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
                AddError($"The control should contain {count} elements with tag {elementTag}.");
                return DothtmlControlCollection.Inactive;
            }
        }

        private void AddError(string message)
        {
            validate.AddError(message, control.DothtmlNode.StartPosition, control.DothtmlNode.EndPosition);
        }

        private void AddError(string message, int startPosition, int endPosition)
        {
            validate.AddError(message, startPosition, endPosition);
        }

        private DothtmlControlCollection GetControls(string controlType)
        {
            return new DothtmlControlCollection(control.Content
                .Where(c => c.Metadata.Name == controlType)
                .Select(c => new DothtmlControl(c, validate)));
        }

        private DothtmlControlCollection GetDothtmlControls(Func<DothtmlControlCollection> activeAction)
        {
            return IsActive ? activeAction() : DothtmlControlCollection.Inactive;
        }

        private DothtmlControlCollection GetElements(string elementTag)
        {
            return new DothtmlControlCollection(control.Content
                .Where(c => c.DothtmlNode is DothtmlElementNode)
                .Where(c => ((DothtmlElementNode)c.DothtmlNode).TagName == elementTag)
                .Select(c => new DothtmlControl(c, validate)));
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