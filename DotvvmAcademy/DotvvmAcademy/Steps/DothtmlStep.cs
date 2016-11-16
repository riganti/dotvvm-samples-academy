using System;
using System.Collections.Generic;
using System.Linq;
using DotvvmAcademy.Lessons;
using DotvvmAcademy.Steps.Validation;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Tokenizer;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ViewModel;

namespace DotvvmAcademy.Steps
{
    public class DothtmlStep : CodeBaseStep
    {
        public DothtmlStep(LessonBase currentLesson) : base(currentLesson)
        {
        }

        //TODO: REMOVE

        [Bind(Direction.None)]
        public Action<ResolvedTreeRoot> ValidationFunction { get; set; }

        protected override IEnumerable<string> GetErrors()
        {
            try
            {
                ResolvedTreeRoot root;
                try
                {
                    var tokenizer = new DothtmlTokenizer();
                    tokenizer.Tokenize(Code);
                    var parser = new DothtmlParser();
                    var node = parser.Parse(tokenizer.Tokens);
                    var resolver = new DefaultControlTreeResolver(DotvvmConfiguration.CreateDefault());
                    root = (ResolvedTreeRoot) resolver.ResolveTree(node, Guid.NewGuid() + ".dothtml");
                }
                catch (Exception ex)
                {
                    throw new CodeValidationException("Syntax error in the DOTHTML code.", ex);
                }

                //TODO: remove this function
                //TODO: get validation function when constructor is called by searching methods in "LessonBase" where function has the LessonStepValidation attribute with right key.
                ValidationFunction(root);

                return Enumerable.Empty<string>();
            }
            catch (CodeValidationException ex)
            {
                return new[] {ex.Message};
            }
        }
    }
}