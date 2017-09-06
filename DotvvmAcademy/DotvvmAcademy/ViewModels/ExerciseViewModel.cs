using DotVVM.Framework.ViewModel;
using DotvvmAcademy.BL.Dtos;
using DotvvmAcademy.BL.Facades;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.ViewModels
{
    public class ExerciseViewModel
    {
        public string Code { get; set; }

        [Protect(ProtectMode.EncryptData)]
        public ExerciseDto Dto { get; set; }

        public List<ValidationErrorDto> Errors { get; set; }

        [Protect(ProtectMode.EncryptData)]
        public bool IsValid { get; set; }

        [Bind(Direction.None)]
        public ValidatorFacade ValidatorFacade { get; set; }

        [Protect(ProtectMode.EncryptData)]
        public string ValidatorId { get; set; }

        public void ResetCode()
        {
            Code = Dto.Incorrect;
            Errors.Clear();
            IsValid = false;
        }

        public void ShowCorrectCode()
        {
            Code = Dto.Correct;
            Errors.Clear();
            IsValid = true;
        }

        public async Task Validate()
        {
            Errors.Clear();
            Errors = (await ValidatorFacade.Validate(Dto, Code)).ToList();
            IsValid = Errors.Any();
        }
    }
}