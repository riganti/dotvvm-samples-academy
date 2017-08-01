using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;

namespace DotvvmAcademy.Steps.Validation.Interfaces
{
    public interface ICSharpCodeValidationObject : ILessonValidationObject
    {
        void Validate(CSharpCompilation compilation, CSharpSyntaxTree tree, SemanticModel model,
            Assembly assembly);
    }
}