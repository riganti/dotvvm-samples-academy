using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DotvvmAcademy.Steps.Validation.Validators.CommonValidators
{
    public static class CsharpCommonValidator
    {
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


        public static void GetVoidMethodsValidationErrors(List<IMethodSymbol> voidMethods, List<string> voidMethodsNames)
        {
            foreach (var methodName in voidMethodsNames)
            {
                GetVoidMethodValidationError(voidMethods, methodName);
            }
        }

        public static void GetVoidMethodValidationError(List<IMethodSymbol> voidMethods, string methodName)
        {
            if (!voidMethods.Any(m => m.CheckNameAndVoid(methodName)))
            {
                throw new CodeValidationException(string.Format(ValidationErrorMessages.MethodNotFound, methodName));
            }
        }


        public static void GetPropertiesValidationErrors(List<IPropertySymbol> treeProperties,
            List<Property> propertiesToValidate)
        {
            foreach (var prop in propertiesToValidate)
            {
                GetPropertyValidationError(treeProperties, prop);
            }
        }

        public static void GetPropertyValidationError(List<IPropertySymbol> treeProperties, Property propertyToValidate)
        {
            if (!treeProperties.Any(p => p.CheckNameAndType(propertyToValidate.Name, propertyToValidate.Type)))
            {
                throw new CodeValidationException(string.Format(ValidationErrorMessages.PropertyNotFound,
                    propertyToValidate.Name));
            }
        }


        public static void GetVoidClassesValidationErrors(List<INamedTypeSymbol> voidMethods, List<string> classesNames)
        {
            foreach (var className in classesNames)
            {
                GetClassValidationError(voidMethods, className);
            }
        }

        public static void GetClassValidationError(List<INamedTypeSymbol> classDeclarations, string className)
        {
            if (classDeclarations.All(c => c.Name != className))
            {
                throw new CodeValidationException(string.Format(ValidationErrorMessages.ClassNotFound, className));
            }
        }
    }
}