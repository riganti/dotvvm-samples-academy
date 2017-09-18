using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Tokenizer;
using DotVVM.Framework.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DotvvmAcademy.Validation.Dothtml
{
    public sealed class DothtmlValidate : Validate
    {
        public DothtmlValidate(MethodInfo validator, string code, IEnumerable<string> dependencies) : base(validator, code, dependencies)
        {
        }

        public DothtmlRoot Root { get; private set; }

        protected override void Init()
        {
            try
            {
                var tokenizer = new DothtmlTokenizer();
                var parser = new DothtmlParser();
                var configuration = DotvvmConfiguration.CreateDefault();
                var resolver = new DefaultControlTreeResolver(configuration);

                tokenizer.Tokenize(Code);
                var root = (ResolvedTreeRoot)resolver.ResolveTree(parser.Parse(tokenizer.Tokens), GetFileName());
                Root = new DothtmlRoot(this, root);
                AddResolverErrors(root.DothtmlNode);
            }
            catch(Exception e)
            {
                AddGlobalError($"An exception occured during dothtml compilation: '{e}'.");
                Root = DothtmlRoot.Inactive;
            }
        }

        private void AddResolverErrors(DothtmlNode node)
        {
            foreach (var error in node.NodeErrors)
            {
                // TODO add special kind of errors for this
                AddError(error, node.StartPosition, node.EndPosition, null);
            }

            if (node is DothtmlNodeWithContent contentNode)
            {
                foreach (var child in contentNode.Content)
                {
                    AddResolverErrors(child);
                }
            }
        }

        private string GetFileName()
        {
            return $"View{Id}.dothtml";
        }
    }
}