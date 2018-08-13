using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.CSharp.Experiments
{
    [TestClass]
    public class SyntaxTreeExperiments
    {
        [TestMethod]
        public void ParsedSpanExperiment()
        {
            var tree = CSharpSyntaxTree.ParseText("class Test {}");
        }

        [TestMethod]
        public void FactorySpanExperiment()
        {
            var unit = SyntaxFactory.CompilationUnit()
                .WithMembers(SyntaxFactory.List<MemberDeclarationSyntax>()
                    .Add(SyntaxFactory.ClassDeclaration(SyntaxFactory.Identifier("Test"))));
        }
    }
}
