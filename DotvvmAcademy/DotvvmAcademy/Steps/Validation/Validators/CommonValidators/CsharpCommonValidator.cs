using DotvvmAcademy.Steps.Validation.Validators.PropertyAndControlType;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.Steps.Validation.Validators.CommonValidators
{
    public static class CSharpCommonValidator
    {
        public static List<INamedTypeSymbol> GetClassDeclarations(CSharpSyntaxTree tree, SemanticModel model)
        {
            return tree.GetCompilationUnitRoot().DescendantNodes().OfType<ClassDeclarationSyntax>()
                .Select(c => model.GetDeclaredSymbol(c))
                .Where(c => c.DeclaredAccessibility == Accessibility.Public)
                .ToList();
        }

        public static List<IMethodSymbol> GetTreeMethods(CSharpSyntaxTree tree, SemanticModel model)
        {
            return tree.GetCompilationUnitRoot().DescendantNodes().OfType<MethodDeclarationSyntax>()
                .Select(p => model.GetDeclaredSymbol(p))
                .ToList();
        }

        public static List<IPropertySymbol> GetTreeProperties(CSharpSyntaxTree tree, SemanticModel model)
        {
            return tree.GetCompilationUnitRoot().DescendantNodes().OfType<PropertyDeclarationSyntax>()
                .Select(p => model.GetDeclaredSymbol(p))
                .ToList();
        }

        public static void ValidateClass(CSharpSyntaxTree tree, SemanticModel model, string className)
        {
            var classDeclarations = GetClassDeclarations(tree, model);
            GetClassValidationError(classDeclarations, className);
        }

        public static void ValidateClasses(CSharpSyntaxTree tree, SemanticModel model, List<string> classesNames)
        {
            var classDeclarations = GetClassDeclarations(tree, model);
            foreach (var className in classesNames)
            {
                GetClassValidationError(classDeclarations, className);
            }
        }

        public static void ValidateClassIfImplementInterface(CSharpSyntaxTree tree, SemanticModel model, string className, string interfaceName)
        {
            ValidateClass(tree, model, className);
            INamedTypeSymbol classDeclarations = GetClassDeclarations(tree, model).FirstOrDefault(cd => cd.Name == className);

            if (!classDeclarations.AllInterfaces.Any(i => i.Name == interfaceName))
            {
                throw new CodeValidationException(string.Format(ValidationErrorMessages.ClassDoesNotImplementInterface, className, interfaceName));
            }
        }

        public static void ValidateMethod(CSharpSyntaxTree tree, SemanticModel model, string methodName)
        {
            var treeMethods = GetTreeMethods(tree, model);
            GetVoidMethodValidationError(treeMethods, methodName);
        }

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

        private static void GetClassValidationError(List<INamedTypeSymbol> classTreeDeclarations, string className)
        {
            if (!classTreeDeclarations.Any(c => c.Name == className))
            {
                throw new CodeValidationException(string.Format(ValidationErrorMessages.ClassNotFound, className));
            }
        }

        private static void GetPropertyValidationError(List<IPropertySymbol> treeProperties, Property propertyToValidate)
        {
            if (!treeProperties.Any(p => p.CheckNameAndType(propertyToValidate.Name, propertyToValidate.CsharpType)))
            {
                throw new CodeValidationException(string.Format(ValidationErrorMessages.PropertyNotFound,
                    propertyToValidate.Name));
            }
        }

        private static void GetVoidMethodValidationError(List<IMethodSymbol> voidMethods, string methodName)
        {
            if (!voidMethods.Any(m => m.CheckNameAndVoid(methodName)))
            {
                throw new CodeValidationException(string.Format(ValidationErrorMessages.MethodNotFound, methodName));
            }
        }
    }
}