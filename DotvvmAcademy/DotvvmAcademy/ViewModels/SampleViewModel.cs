using DotVVM.Framework.ViewModel;
using DotvvmAcademy.BL.DTO;
using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.ViewModels
{
    public class SampleViewModel
    {
        public string Code { get; set; }

        [Bind(Direction.None)]
        public SampleDTO DTO { get; set; }

        public List<ValidationErrorViewModel> Errors { get; set; } = new List<ValidationErrorViewModel>();

        public bool IsValid { get; set; }

        public static SampleViewModel Create(SampleDTO dto)
        {
            var sample = new SampleViewModel
            {
                DTO = dto
            };
            return sample;
        }

        public void ResetCode()
        {
            Code = DTO.IncorrectCode;
            IsValid = false;
        }

        public void ShowCorrectCode()
        {
            Code = DTO.CorrectCode;
            IsValid = true;
        }

        public void Validate()
        {
            Errors.Clear();
            Errors.AddRange(DTO.Validate(Code).Select(e => ValidationErrorViewModel.Create(e)));
            IsValid = Errors.Any();
        }
    }
}