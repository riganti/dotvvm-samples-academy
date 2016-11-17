using System.Linq;
using System.Reflection;
using DotvvmAcademy.Lessons;
using DotvvmAcademy.Steps.Validation.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DotvvmAcademy.Steps.Validation.Validators.Lesson2
{
    [StepValidation(ValidationKey = "Lesson2Step5Validator")]
    public class Lesson2Step5Validator : ICSharpCodeStepValidationObject
    {
        public void Validate(CSharpCompilation compilation, CSharpSyntaxTree tree, SemanticModel model, Assembly assembly)
        {
            var classDeclarations = tree.GetCompilationUnitRoot().DescendantNodes().OfType<ClassDeclarationSyntax>()
               .Select(c => model.GetDeclaredSymbol(c))
               .ToList();
            if (classDeclarations.Count(c => c.Name == "TaskData") != 1)
            {
                throw new CodeValidationException(string.Format(GenericTexts.ClassNotFound, "TaskData"));
            }

            var properties = tree.GetCompilationUnitRoot().DescendantNodes().OfType<PropertyDeclarationSyntax>()
                .Select(p => model.GetDeclaredSymbol(p))
                .ToList();
            if (properties.Count(p => p.CheckNameAndType("Title", "string")) != 1)
            {
                throw new CodeValidationException(string.Format(GenericTexts.PropertyNotFound, "Title"));
            }
            if (properties.Count(p => p.CheckNameAndType("IsCompleted", "bool")) != 1)
            {
                throw new CodeValidationException(string.Format(GenericTexts.PropertyNotFound, "IsCompleted"));
            }
        }
    }
}