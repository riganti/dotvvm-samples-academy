using DotVVM.Framework.Compilation.ControlTree.Resolved;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class NodeLocator
    {
        private readonly ResolvedTreeRoot root;

        public NodeLocator(ResolvedTreeRoot root)
        {
            this.root = root;
        }

        public bool TryLocate(DothtmlIdentifier identifier, out ResolvedContentNode control)
        {
            throw new NotImplementedException();
            //ResolvedContentNode parent = null;
            //if (identifier.Parent == null)
            //{
            //    parent = root;
            //}
            //else if (TryLocate(identifier.Parent, out parent))
            //{
            //    parent.
            //}
            //control = null;
            //return false;
        }
    }
}
