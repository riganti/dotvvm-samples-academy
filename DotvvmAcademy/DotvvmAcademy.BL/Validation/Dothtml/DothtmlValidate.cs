using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Tokenizer;
using DotVVM.Framework.Configuration;
using System;
using System.Linq;

namespace DotvvmAcademy.BL.Validation.Dothtml
{
    public sealed class DothtmlValidate : Validate
    {
        private ResolvedTreeRoot root;

        public DothtmlValidate(string code) : base(code)
        {
        }

        public DothtmlControl Root { get; private set; }

        protected override void Init()
        {
            var tokenizer = new DothtmlTokenizer();
            var parser = new DothtmlParser();
            var configuration = DotvvmConfiguration.CreateDefault();
            var resolver = new DefaultControlTreeResolver(configuration);

            tokenizer.Tokenize(Code);
            root = (ResolvedTreeRoot)resolver.ResolveTree(parser.Parse(tokenizer.Tokens), GetFileName());
            Root = new DothtmlControl(root, this);
        }

        private string GetFileName()
        {
            return $"{Guid.NewGuid()}.dothtml";
        }
    }
}