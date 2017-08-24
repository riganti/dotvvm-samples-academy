using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Tokenizer;
using DotVVM.Framework.Configuration;
using System;
using System.Collections.Generic;

namespace DotvvmAcademy.BL.Validation.Dothtml
{
    public sealed class DothtmlValidate : Validate
    {
        public DothtmlValidate(string code, IEnumerable<string> dependencies) : base(code, dependencies)
        {
        }

        public DothtmlRoot Root { get; private set; }

        protected override void Init()
        {
            var tokenizer = new DothtmlTokenizer();
            var parser = new DothtmlParser();
            var configuration = DotvvmConfiguration.CreateDefault();
            var resolver = new DefaultControlTreeResolver(configuration);

            tokenizer.Tokenize(Code);
            var root = (ResolvedTreeRoot)resolver.ResolveTree(parser.Parse(tokenizer.Tokens), GetFileName());
            Root = new DothtmlRoot(this, root);
            AddCompilationErrors(root.DothtmlNode);
        }

        private void AddCompilationErrors(DothtmlNode node)
        {
            foreach (var error in node.NodeErrors)
            {
                AddError(error, node.StartPosition, node.EndPosition);
            }

            if(node is DothtmlNodeWithContent contentNode)
            {
                foreach (var child in contentNode.Content)
                {
                    AddCompilationErrors(child);
                }
            }
        }

        private string GetFileName()
        {
            return $"View{Id}.dothtml";
        }
    }
}