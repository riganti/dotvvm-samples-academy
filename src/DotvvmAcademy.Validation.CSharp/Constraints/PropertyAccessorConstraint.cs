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
                        message: GetDiagnosticMessage(),
                        arguments: new object[] { property, AllowedAccess },
                        symbol: (ISymbol)accessor ?? property);
                }
            }
        }

        private IMethodSymbol GetAccessor(IPropertySymbol property)
        {
            return Kind switch
            {
                AccessorKind.Get => property.GetMethod,
                AccessorKind.Set => property.SetMethod,
                _ => throw new InvalidOperationException("AccessorKind is invalid."),
            };
        }

        private string GetDiagnosticMessage()
        {
            return Kind switch
            {
                AccessorKind.Get => Resources.ERR_MissingGetter,
                AccessorKind.Set => Resources.ERR_MissingSetter,
                _ => throw new InvalidOperationException("AccessorKind is invalid."),
            };
        }
    }
}
