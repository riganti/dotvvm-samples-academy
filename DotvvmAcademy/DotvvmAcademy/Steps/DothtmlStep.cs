using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Tokenizer;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ViewModel;
using DotvvmAcademy.Steps.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.Steps
{
    public class DothtmlStep : StepBase
    {

        public string Code { get; set; } = "";


        [Bind(Direction.None)]
        public string StartupCode { get; set; }

        [Bind(Direction.None)]
        public string FinalCode { get; set; }

        [Bind(Direction.None)]
        public Action<ResolvedTreeRoot> ValidationFunction { get; set; }
        public string Description2 { get; internal set; }

        protected override IEnumerable<string> GetErrors()
        {
            var tokenizer = new DothtmlTokenizer();
            tokenizer.Tokenize(Code);
            var parser = new DothtmlParser();
            var node = parser.Parse(tokenizer.Tokens);
            var resolver = new DotVVM.Framework.Compilation.ControlTree.DefaultControlTreeResolver(DotvvmConfiguration.CreateDefault());
            var root = (ResolvedTreeRoot)resolver.ResolveTree(node, Guid.NewGuid().ToString() + ".dothtml");
            try
            {
                ValidationFunction(root);
                return Enumerable.Empty<string>();
            }
            catch (CodeValidationException ex)
            {
                return new[] { ex.Message };
            }
        }
    }
}
