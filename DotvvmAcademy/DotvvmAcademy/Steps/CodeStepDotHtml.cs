using System;
using System.Collections.Generic;
using System.Linq;
using DotvvmAcademy.Lessons;
using DotvvmAcademy.Steps.StepsBases;
using DotvvmAcademy.Steps.Validation;
using DotvvmAcademy.Steps.Validation.Validators;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Tokenizer;
using DotVVM.Framework.Configuration;

namespace DotvvmAcademy.Steps
{
    public class CodeStepDotHtml : CodeStepBase<IDotHtmlCodeStepValidationObject>
    {
        public CodeStepDotHtml(LessonBase currentLesson) : base(currentLesson)
        {
        }

        public override IDotHtmlCodeStepValidationObject Validator { get; set; }

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
                Validator.ValidateMethod(root);

                return Enumerable.Empty<string>();
            }
            catch (CodeValidationException ex)
            {
                return new[] {ex.Message};
            }
        }
    }
}