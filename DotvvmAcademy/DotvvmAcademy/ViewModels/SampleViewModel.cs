using DotVVM.Framework.ViewModel;
using DotvvmAcademy.BL.DTO;
using DotvvmAcademy.BL.Facades;
using DotvvmAcademy.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.ViewModels
{
    public class SampleViewModel : DotvvmViewModelBase
    {
        public string Code { get; set; }

        [Bind(Direction.None)]
        public ValidatorFacade ValidatorFacade { get; set; }

        [Bind(Direction.None)]
        public ValidatorsBuilder ValidatorsBuilder { get; set; }

        [Bind(Direction.None)]
        public SampleDTO DTO { get; set; }

        public List<ValidationErrorViewModel> Errors { get; set; } = new List<ValidationErrorViewModel>();

        public bool IsValid { get; set; }

        public void ResetCode()
        {
            Code = DTO.IncorrectCode;
            Errors.Clear();
            IsValid = false;
        }

        public void ShowCorrectCode()
        {
            Code = DTO.CorrectCode;
            IsValid = true;
        }

        public async Task Validate()
        {
            Errors.Clear();
            var errorDtos = await ValidatorFacade.Validate(DTO, ValidatorsBuilder.ValidatorAssemblyPath, Code);
            Errors.AddRange(errorDtos.Select(e=> ValidationErrorViewModel.Create(e)));
            IsValid = Errors.Any();
        }
    }
}