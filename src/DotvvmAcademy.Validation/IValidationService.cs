using DotvvmAcademy.Validation.Unit;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation
{
    public interface IValidationService<TUnit, TOptions>
        where TUnit : class, IUnit
        where TOptions : class, IValidationOptions
    {
        Task<ImmutableArray<IValidationDiagnostic>> Validate(TUnit unit, string code, TOptions options = null);
    }
}