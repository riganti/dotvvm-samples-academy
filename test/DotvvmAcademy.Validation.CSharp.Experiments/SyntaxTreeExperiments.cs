using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Xunit;

namespace DotvvmAcademy.Validation.CSharp.Experiments
{
    public class SyntaxTreeExperiments
    {
        [Fact]
        public void FactorySpanExperiment()
        {
            var unit = SyntaxFactory.CompilationUnit()
                .WithMembers(SyntaxFactory.List<MemberDeclarationSyntax>()
                    .Add(SyntaxFactory.ClassDeclaration(SyntaxFactory.Identifier("Test"))));
        }

        [Fact]
        public void ParsedSpanExperiment()
        {
            var tree = CSharpSyntaxTree.ParseText("class Test {}");
        }
    }
}