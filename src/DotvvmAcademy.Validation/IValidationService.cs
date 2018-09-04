using DotvvmAcademy.Validation.Unit;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation
{
    public interface IValidationService<TUnit>
        where TUnit : class, IValidationUnit
    {
        Task<ImmutableArray<IValidationDiagnostic>> Validate(TUnit unit, ImmutableArray<ISourceCode> sources);
    }
}