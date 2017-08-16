using DotVVM.Framework.ViewModel;
using DotvvmAcademy.BL.DTO;
using DotvvmAcademy.BL.Validation;
using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.Models
{
    public class Sample
    {
        public string Code { get; set; }

        [Bind(Direction.None)]
        public SampleDTO DTO { get; set; }

        public List<ValidationError> Errors { get; set; } = new List<ValidationError>();

        public bool IsValid { get; set; }

        public static Sample Create(SampleDTO dto)
        {
            var sample = new Sample
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
            Errors.AddRange(DTO.Validate(Code));
            IsValid = Errors.Any();
        }
    }
}