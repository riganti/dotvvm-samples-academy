using DotVVM.Framework.Compilation.ControlTree.Resolved;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.BL.Validation.Dothtml
{
    public sealed class DothtmlControlCollection : ValidationObject<DothtmlValidate>, IEnumerable<DothtmlControl>
    {
        internal DothtmlControlCollection(DothtmlValidate validate, ImmutableList<DothtmlControl> controls, ResolvedTreeNode parentNode, bool isActive = true)
            : base(validate, isActive)
        {
            Controls = controls ?? ImmutableList.Create<DothtmlControl>();
            ParentNode = parentNode;
        }

        public static DothtmlControlCollection Inactive => new DothtmlControlCollection(null, ImmutableList.Create<DothtmlControl>(), null, false);

        public ImmutableList<DothtmlControl> Controls { get; internal set; }

        public ResolvedTreeNode ParentNode { get; }

        public DothtmlControl this[int i]
        {
            get
            {
                return IsActive ? Controls.ElementAt(i) : DothtmlControl.Inactive;
            }
        }

        public IEnumerator<DothtmlControl> GetEnumerator()
        {
            return Controls.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Controls.GetEnumerator();
        }

        public override void AddError(string message) => AddError(message, ParentNode.DothtmlNode.StartPosition, ParentNode.DothtmlNode.EndPosition);
    }
}