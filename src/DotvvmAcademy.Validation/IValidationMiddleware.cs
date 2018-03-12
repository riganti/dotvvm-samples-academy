using System.Threading.Tasks;

namespace DotvvmAcademy.Validation
{
    public interface IValidationMiddleware
    {
        Task InvokeAsync(ValidationContext context, ValidationDelegate next);
    }
}