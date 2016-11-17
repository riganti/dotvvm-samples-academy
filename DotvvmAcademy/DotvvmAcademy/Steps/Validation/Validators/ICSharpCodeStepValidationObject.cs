using System.Reflection;
using DotvvmAcademy.Steps.Validation.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DotvvmAcademy.Steps.Validation.Validators
{
    public interface ICSharpCodeStepValidationObject : ILessonValidationObject
    {
        void ValidationFunction(CSharpCompilation compilation, CSharpSyntaxTree tree, SemanticModel model,
            Assembly assembly);
    }
}

