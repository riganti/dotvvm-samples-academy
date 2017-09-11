using DotVVM.Framework.ViewModel;
using DotvvmAcademy.BL.Dtos;
using DotvvmAcademy.BL.Facades;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.ViewModels
{
    public class ExerciseViewModel : DotvvmViewModelBase
    {
        private SampleFacade sampleFacade;
        private ValidatorFacade validatorFacade;

        public ExerciseViewModel()
        {
        }

        public ExerciseViewModel(ValidatorFacade validatorFacade, SampleFacade sampleFacade)
        {
            this.validatorFacade = validatorFacade;
            this.sampleFacade = sampleFacade;
        }

        public void InjectServices(ValidatorFacade validatorFacade, SampleFacade sampleFacade)
        {
            // This is because dotvvm doesn't resolve the properties it creates, it expects them to have a parameterless constructor
            this.validatorFacade = validatorFacade;
            this.sampleFacade = sampleFacade;
        }

        public string Code { get; set; }

        [Bind(Direction.None)]
        public ExerciseBaseDto Dto { get; set; }

        public List<ValidationErrorDto> Errors { get; set; } = new List<ValidationErrorDto>();

        [Protect(ProtectMode.EncryptData)]
        public int Index { get; set; }

        [Protect(ProtectMode.EncryptData)]
        public bool IsValid { get; set; }

        public async Task ResetCode()
        {
            var sample = await sampleFacade.GetSample(Dto.IncorrectPath);
            Code = sample.Source;
            Errors.Clear();
            IsValid = false;
        }

        public async Task ShowCorrectCode()
        {
            var sample = await sampleFacade.GetSample(Dto.CorrectPath);
            Code = sample.Source;
            Errors.Clear();
            IsValid = true;
        }

        public async Task Validate()
        {
            Errors.Clear();
            Errors = (await validatorFacade.Validate(Dto, Code)).ToList();
            IsValid = Errors.Any();
        }
    }
}