using DotvvmAcademy.Meta;
using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using System;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp.Constraints
{
    internal class PropertyAccessorConstraint
    {
        public PropertyAccessorConstraint(NameNode node, AccessorKind kind, AllowedAccess allowedAccess)
        {
            Node = node;
            Kind = kind;
            AllowedAccess = allowedAccess;
        }

        public enum AccessorKind
        {
            Get,
            Set,
        }

        public AllowedAccess AllowedAccess { get; }

        public AccessorKind Kind { get; }

        public NameNode Node { get; }

        public void Validate(IValidationReporter reporter, MetaConverter converter)
        {
            var properties = converter.ToRoslyn(Node)
                .OfType<IPropertySymbol>();
            foreach(var property in properties)
            {
                var accessor = GetAccessor(property);
                if (accessor == null || !AllowedAccess.HasFlag(accessor.DeclaredAccessibility.ToAllowedAccess()))
                {
                    reporter.Report(
                        message: GetDiagnosticMessage(property),
                        arguments: new object[] { property, AllowedAccess },
                        symbol: (ISymbol)accessor ?? property);
                }
            }
        }

        private IMethodSymbol GetAccessor(IPropertySymbol property)
        {
            switch (Kind)
            {
                case AccessorKind.Get:
                    return property.GetMethod;
                case AccessorKind.Set:
                    return property.SetMethod;
                default:
                    throw new InvalidOperationException("AccessorKind is invalid.");
            }
        }

        private string GetDiagnosticMessage(IPropertySymbol property)
        {
            switch (Kind)
            {
                case AccessorKind.Get:
                    return Resources.ERR_MissingGetter;
                case AccessorKind.Set:
                    return Resources.ERR_MissingSetter;
                default:
                    throw new InvalidOperationException("AccessorKind is invalid.");
            }
        }
    }
}