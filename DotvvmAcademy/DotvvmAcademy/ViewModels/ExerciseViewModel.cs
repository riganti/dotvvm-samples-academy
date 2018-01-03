using DotVVM.Framework.ViewModel;
using DotvvmAcademy.BL.Dtos;
using DotvvmAcademy.BL.Facades;
using DotvvmAcademy.Controls;
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
            // dotvvm requires a parameterless constructor
            this.validatorFacade = validatorFacade;
            this.sampleFacade = sampleFacade;
        }

        public string Code { get; set; }

        [Bind(Direction.None)]
        public ExerciseStepPartDto Dto { get; set; }

        public List<ValidationErrorDto> Errors { get; set; } = new List<ValidationErrorDto>();

        public List<MonacoMarker> Markers { get; set; }

        [Bind(Direction.ServerToClientFirstRequest)]
        public MonacoLanguage CodeLanguage => Dto.CodeLanguage.ToMonacoLanguage();

        [Protect(ProtectMode.EncryptData)]
        public int Index { get; set; }

        [Protect(ProtectMode.EncryptData)]
        public bool IsValid { get; set; }

        public async Task ResetCode()
        {
            var sample = await sampleFacade.GetSample(Dto.InitialPath);
            Code = sample.Source;
            Errors.Clear();
            IsValid = false;
        }

        public async Task ShowCorrectCode()
        {
            var sample = await sampleFacade.GetSample(Dto.FinalPath);
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