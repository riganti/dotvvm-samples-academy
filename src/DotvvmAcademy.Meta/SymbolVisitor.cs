using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotvvmAcademy.Meta
{
    internal class SymbolVisitor<TResult>
    {
        public virtual TResult Visit(ISymbol symbol)
        {
            switch (symbol)
            {
                case IAliasSymbol aliasSymbol:
                    return VisitAlias(aliasSymbol);
                case IArrayTypeSymbol arrayTypeSymbol:
                    return VisitArrayType(arrayTypeSymbol);
                case IAssemblySymbol assemblySymbol:
                    return VisitAssembly(assemblySymbol);
                case IDiscardSymbol discardSymbol:
                    return VisitDiscard(discardSymbol);
                case IDynamicTypeSymbol dynamicTypeSymbol:
                    return VisitDynamicType(dynamicTypeSymbol);
                case IErrorTypeSymbol errorTypeSymbol:
                    return VisitErrorType(errorTypeSymbol);
                case IEventSymbol eventSymbol:
                    return VisitEvent(eventSymbol);
                case IFieldSymbol fieldSymbol:
                    return VisitField(fieldSymbol);
                case ILabelSymbol labelSymbol:
                    return VisitLabel(labelSymbol);
                case ILocalSymbol localSymbol:
                    return VisitLocal(localSymbol);
                case IMethodSymbol methodSymbol:
                    return VisitMethod(methodSymbol);
                case IModuleSymbol moduleSymbol:
                    return VisitModule(moduleSymbol);
                case INamedTypeSymbol namedTypeSymbol:
                    return VisitNamedType(namedTypeSymbol);
                case INamespaceSymbol namespaceSymbol:
                    return VisitNamespace(namespaceSymbol);
                case IParameterSymbol parameterSymbol:
                    return VisitParameter(parameterSymbol);
                case IPointerTypeSymbol pointerSymbol:
                    return VisitPointerType(pointerSymbol);
                case IPreprocessingSymbol preprocessingSymbol:
                    return VisitPreprocessing(preprocessingSymbol);
                case IPropertySymbol propertySymbol:
                    return VisitProperty(propertySymbol);
                case IRangeVariableSymbol rangeVariableSymbol:
                    return VisitRangeVariable(rangeVariableSymbol);
                case ITypeParameterSymbol typeParameterSymbol:
                    return VisitTypeParameter(typeParameterSymbol);
                default:
                    throw new InvalidOperationException($"\"{symbol.GetType()}\" is not a supported ISymbol.");
            }
        }

        public virtual TResult DefaultVisit(ISymbol symbol)
        {
            return default;
        }

        public virtual TResult VisitAlias(IAliasSymbol symbol)
        {
            return DefaultVisit(symbol);
        }

        public virtual TResult VisitArrayType(IArrayTypeSymbol symbol)
        {
            return DefaultVisit(symbol);
        }

        public virtual TResult VisitAssembly(IAssemblySymbol symbol)
        {
            return DefaultVisit(symbol);
        }

        public virtual TResult VisitDiscard(IDiscardSymbol symbol)
        {
            return DefaultVisit(symbol);
        }

        public virtual TResult VisitDynamicType(IDynamicTypeSymbol symbol)
        {
            return DefaultVisit(symbol);
        }

        public virtual TResult VisitErrorType(IErrorTypeSymbol symbol)
        {
            return DefaultVisit(symbol);
        }

        public virtual TResult VisitEvent(IEventSymbol symbol)
        {
            return DefaultVisit(symbol);
        }

        public virtual TResult VisitField(IFieldSymbol symbol)
        {
            return DefaultVisit(symbol);
        }

        public virtual TResult VisitLabel(ILabelSymbol symbol)
        {
            return DefaultVisit(symbol);
        }

        public virtual TResult VisitLocal(ILocalSymbol symbol)
        {
            return DefaultVisit(symbol);
        }

        public virtual TResult VisitMethod(IMethodSymbol symbol)
        {
            return DefaultVisit(symbol);
        }

        public virtual TResult VisitModule(IModuleSymbol symbol)
        {
            return DefaultVisit(symbol);
        }

        public virtual TResult VisitNamedType(INamedTypeSymbol symbol)
        {
            return DefaultVisit(symbol);
        }

        public virtual TResult VisitNamespace(INamespaceSymbol symbol)
        {
            return DefaultVisit(symbol);
        }

        public virtual TResult VisitParameter(IParameterSymbol symbol)
        {
            return DefaultVisit(symbol);
        }

        public virtual TResult VisitPointerType(IPointerTypeSymbol symbol)
        {
            return DefaultVisit(symbol);
        }

        public virtual TResult VisitPreprocessing(IPreprocessingSymbol symbol)
        {
            return DefaultVisit(symbol);
        }

        public virtual TResult VisitProperty(IPropertySymbol symbol)
        {
            return DefaultVisit(symbol);
        }

        public virtual TResult VisitRangeVariable(IRangeVariableSymbol symbol)
        {
            return DefaultVisit(symbol);
        }

        public virtual TResult VisitTypeParameter(ITypeParameterSymbol symbol)
        {
            return DefaultVisit(symbol);
        }
    }
}
