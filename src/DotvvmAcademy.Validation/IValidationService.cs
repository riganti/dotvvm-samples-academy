using System.Threading.Tasks;

namespace DotvvmAcademy.Validation
{
    public interface IValidationService
    {
        Task Validate(ValidationContext context);
    }
}
