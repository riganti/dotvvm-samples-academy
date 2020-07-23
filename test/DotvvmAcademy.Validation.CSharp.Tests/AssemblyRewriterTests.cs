using DotvvmAcademy.Meta;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.IO;
using System.Runtime.Loader;
using Xunit;

namespace DotvvmAcademy.Validation.CSharp.Tests
{
    public class AssemblyRewriterTests
    {
        [Fact]
        public void AssemblyRewriter_ExpressionTree_NoException()
        {
            var tree = CSharpSyntaxTree.ParseText(
@"using System;
using System.Linq.Expressions;

public class Test
{
    public int Property { get; set; }
    
    public object One<TProp>(Expression<Func<Test, TProp>> function)
    {
        return new object();
    }

    public void Two()
    {
        var test = true;
        if(test)
        {
            One(vm => vm.Property);
        }
    }
}
");
            var compilation = CSharpCompilation.Create(
                "Test",
                new[] { tree },
                new[]
                {
                    RoslynReference.FromName("System.Private.CoreLib"),
                    RoslynReference.FromName("netstandard"),
                    RoslynReference.FromName("System.Linq.Expressions"),
                    RoslynReference.FromName("System.Runtime"),
                },
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            using var originalStream = new MemoryStream();
            using var rewrittenStream = new MemoryStream();
            var result = compilation.Emit(originalStream);
            Assert.True(result.Success);
            originalStream.Position = 0;
            var rewriter = new AssemblyRewriter();
            rewriter.Rewrite(originalStream, rewrittenStream);
            rewrittenStream.Position = 0;
            var test = AssemblyLoadContext.Default.LoadFromStream(rewrittenStream);
            var type = test.GetType("Test");
            var instance = Activator.CreateInstance(type);
            type.GetMethod("Two")
                .Invoke(instance, null);
        }
    }
}
