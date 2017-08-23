using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Tokenizer;
using DotVVM.Framework.Configuration;
using System;

namespace DotvvmAcademy.BL.Validation.Dothtml
{
    public sealed class DothtmlValidate : Validate
    {
        public DothtmlValidate(string code) : base(code)
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
        }

        private string GetFileName()
        {
            return $"{Guid.NewGuid()}.dothtml";
        }
    }
}