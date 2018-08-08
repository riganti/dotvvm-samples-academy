using DotvvmAcademy.Validation.Unit;
using Microsoft.Extensions.Options;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation
{
    public interface IValidationService<TUnit, TOptions>
        where TUnit : class, IUnit
        where TOptions : class, IOptions<TOptions>, new()
    {
        Task<ImmutableArray<IValidationDiagnostic>> Validate(TUnit unit, string code, IOptions<TOptions> options = default);
    }
}