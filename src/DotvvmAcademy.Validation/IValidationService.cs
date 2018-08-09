using DotvvmAcademy.Validation.Unit;
using Microsoft.Extensions.Options;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation
{
    public interface IValidationService<TUnit>
        where TUnit : class, IUnit
    {
        Task<ImmutableArray<IValidationDiagnostic>> Validate(TUnit unit, ImmutableArray<ISourceCode> sources);
    }
}