using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DotvvmAcademy.Steps.Validation.Validators.CommonValidators
{
    public static class CsharpCommonValidator
    {
        public static void ValidateProperties(CSharpSyntaxTree tree, SemanticModel model,
            List<Property> propertiesToValidate)
        {
            var treeProperties = GetTreeProperties(tree, model);
            foreach (var propertyToValidate in propertiesToValidate)
            {
                GetPropertyValidationError(treeProperties, propertyToValidate);
            }
        }

        public static void ValidateProperty(CSharpSyntaxTree tree, SemanticModel model, Property propertyToValidate)
        {
            var treeProperties = GetTreeProperties(tree, model);
            GetPropertyValidationError(treeProperties, propertyToValidate);
        }

        public static void ValidateClasses(CSharpSyntaxTree tree, SemanticModel model, List<string> classesNames)
        {
            var classDeclarations = GetClassDeclarations(tree, model);
            foreach (var className in classesNames)
            {
                GetClassValidationError(classDeclarations, className);
            }
        }

        public static void ValidateClass(CSharpSyntaxTree tree, SemanticModel model, string className)
        {
            var classDeclarations = GetClassDeclarations(tree, model);
            GetClassValidationError(classDeclarations, className);
        }

        public static void ValidateMethod(CSharpSyntaxTree tree, SemanticModel model, string methodName)
        {
            var treeMethods = GetTreeMethods(tree, model);
            GetVoidMethodValidationError(treeMethods, methodName);
        }


        public static List<IPropertySymbol> GetTreeProperties(CSharpSyntaxTree tree, SemanticModel model)
        {
            return tree.GetCompilationUnitRoot().DescendantNodes().OfType<PropertyDeclarationSyntax>()
                .Select(p => model.GetDeclaredSymbol(p))
                .ToList();
        }

        public static List<IMethodSymbol> GetTreeMethods(CSharpSyntaxTree tree, SemanticModel model)
        {
            return tree.GetCompilationUnitRoot().DescendantNodes().OfType<MethodDeclarationSyntax>()
                .Select(p => model.GetDeclaredSymbol(p))
                .ToList();
        }

        public static List<INamedTypeSymbol> GetClassDeclarations(CSharpSyntaxTree tree, SemanticModel model)
        {
            return tree.GetCompilationUnitRoot().DescendantNodes().OfType<ClassDeclarationSyntax>()
                .Select(c => model.GetDeclaredSymbol(c))
                .ToList();
        }

        private static void GetVoidMethodValidationError(List<IMethodSymbol> voidMethods, string methodName)
        {
            if (!voidMethods.Any(m => m.CheckNameAndVoid(methodName)))
            {
                throw new CodeValidationException(string.Format(ValidationErrorMessages.MethodNotFound, methodName));
            }
        }

        private static void GetPropertyValidationError(List<IPropertySymbol> treeProperties, Property propertyToValidate)
        {
            if (!treeProperties.Any(p => p.CheckNameAndType(propertyToValidate.Name, propertyToValidate.Type)))
            {
                throw new CodeValidationException(string.Format(ValidationErrorMessages.PropertyNotFound,
                    propertyToValidate.Name));
            }
        }

        private static void GetClassValidationError(List<INamedTypeSymbol> classTreeDeclarations, string className)
        {
            if (classTreeDeclarations.All(c => c.Name == className))
            {
                throw new CodeValidationException(string.Format(ValidationErrorMessages.ClassNotFound, className));
            }
        }
    }
}