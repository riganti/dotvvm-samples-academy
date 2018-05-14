using DotVVM.Framework.Compilation.ControlTree.Resolved;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class PropertyValueVisitor : IControlVisitor
    {
        public const string PropertyValueKey = "PropertyValue";

        public readonly ValidationDiagnosticDescriptor IncorrectSetterError
             = new ValidationDiagnosticDescriptor("TEMP", "Incorrect setter", "Property '{0}' must be set using a hardcoded value.", ValidationDiagnosticSeverity.Error);

        public readonly ValidationDiagnosticDescriptor MissingValueError
                    = new ValidationDiagnosticDescriptor("TEMP", "Missing value", "Property '{0}' is missing a value.", ValidationDiagnosticSeverity.Error);

        public readonly ValidationDiagnosticDescriptor WrongValue
            = new ValidationDiagnosticDescriptor("TEMP", "Wrong value", "Binding value '{0}' of the '{1}' property is wrong.", ValidationDiagnosticSeverity.Error);

        public List<ValidationDiagnostic> diagnostics = new List<ValidationDiagnostic>();

        public IMetadataCollection<DothtmlIdentifier> Metadata { get; set; }

        public ImmutableArray<ValidationDiagnostic> GetDiagnostics() => diagnostics.ToImmutableArray();

        public void Visit(DothtmlIdentifier identifier, ResolvedControl control)
        {
            var metadata = Metadata.GetRequiredProperty<ImmutableArray<PropertyValueMetadata>>(identifier, PropertyValueKey);
            foreach (var valueMetadata in metadata)
            {
                if (!control.TryGetProperty(valueMetadata.Property, out var setter))
                {
                    diagnostics.Add(ValidationDiagnostic.Create(MissingValueError, new DothtmlValidationDiagnosticLocation(control.DothtmlNode), valueMetadata.Property));
                    return;
                }
                if (!(setter is ResolvedPropertyValue value))
                {
                    diagnostics.Add(ValidationDiagnostic.Create(IncorrectSetterError, new DothtmlValidationDiagnosticLocation(control.DothtmlNode), valueMetadata.Property));
                    return;
                }
                if (!valueMetadata.AcceptedValues.Contains(value.Value))
                {
                    diagnostics.Add(ValidationDiagnostic.Create(WrongValue, new DothtmlValidationDiagnosticLocation(value.DothtmlNode), value.Value, valueMetadata.Property));
                    return;
                }
            }
        }
    }
}