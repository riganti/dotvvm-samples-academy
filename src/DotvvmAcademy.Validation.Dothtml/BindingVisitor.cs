using DotVVM.Framework.Compilation.ControlTree.Resolved;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class BindingVisitor : IControlVisitor
    {
        public const string BindingKey = "Binding";

        public readonly ValidationDiagnosticDescriptor IncorrectBindingTypeError
            = new ValidationDiagnosticDescriptor("TEMP", "Incorrect binding type", "Property '{0}' must be bound using '{1}'.", ValidationDiagnosticSeverity.Error);

        public readonly ValidationDiagnosticDescriptor IncorrectSetterError
             = new ValidationDiagnosticDescriptor("TEMP", "Incorrect setter", "Property '{0}' must be set using a '{1}'.", ValidationDiagnosticSeverity.Error);

        public readonly ValidationDiagnosticDescriptor MissingBindingError
            = new ValidationDiagnosticDescriptor("TEMP", "Missing binding", "Property '{0}' is missing a binding of type '{1}'.", ValidationDiagnosticSeverity.Error);

        public readonly ValidationDiagnosticDescriptor WrongBindingValue
            = new ValidationDiagnosticDescriptor("TEMP", "Wrong binding value", "Binding value '{0}' of the '{1}' property is wrong.", ValidationDiagnosticSeverity.Error);

        public List<ValidationDiagnostic> diagnostics = new List<ValidationDiagnostic>();

        public IMetadataCollection<DothtmlIdentifier> Metadata { get; set; }

        public ImmutableArray<ValidationDiagnostic> GetDiagnostics() => diagnostics.ToImmutableArray();

        public void Visit(DothtmlIdentifier identifier, ResolvedControl control)
        {
            var metadata = Metadata.GetRequiredProperty<ImmutableArray<BindingMetadata>>(identifier, BindingKey);
            foreach (var bindingMetadata in metadata)
            {
                if (!control.TryGetProperty(bindingMetadata.Property, out var setter))
                {
                    diagnostics.Add(ValidationDiagnostic.Create(MissingBindingError, new DothtmlValidationDiagnosticLocation(control.DothtmlNode), bindingMetadata.Property, bindingMetadata.AcceptedValues[0]));
                    return;
                }
                if (!(setter is ResolvedPropertyBinding binding))
                {
                    diagnostics.Add(ValidationDiagnostic.Create(IncorrectSetterError, new DothtmlValidationDiagnosticLocation(control.DothtmlNode), bindingMetadata.Property, bindingMetadata.BindingType));
                    return;
                }
                if (binding.Binding.BindingType != bindingMetadata.BindingType)
                {
                    diagnostics.Add(ValidationDiagnostic.Create(IncorrectBindingTypeError, new DothtmlValidationDiagnosticLocation(binding.DothtmlNode), bindingMetadata.Property, bindingMetadata.BindingType));
                    return;
                }
                if (!bindingMetadata.AcceptedValues.Contains(binding.Binding.Value))
                {
                    diagnostics.Add(ValidationDiagnostic.Create(WrongBindingValue, new DothtmlValidationDiagnosticLocation(binding.DothtmlNode), binding.Binding.Value, bindingMetadata.Property));
                    return;
                }
            }
        }
    }
}