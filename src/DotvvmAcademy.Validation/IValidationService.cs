using DotvvmAcademy.Validation.Unit;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation
{
    public interface IValidationService
    {
        Task<ImmutableArray<IValidationDiagnostic>> Validate(IValidationUnit unit, ImmutableArray<ISourceCode> sources);
    }

    public interface IValidationService<TUnit> : IValidationService
        where TUnit : IValidationUnit
    {
        Task<ImmutableArray<IValidationDiagnostic>> Validate(TUnit unit, ImmutableArray<ISourceCode> sources);
    }
}