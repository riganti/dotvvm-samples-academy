using DotvvmAcademy.BL.DTO;
using DotvvmAcademy.BL.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.BL.Facades
{
    public sealed class ValidatorFacade
    {
        private readonly SampleFacade sampleFacade;
        private readonly ValidatorCli validatorCli;

        public ValidatorFacade(SampleFacade sampleFacade, ValidatorCli validatorCli)
        {
            this.sampleFacade = sampleFacade;
            this.validatorCli = validatorCli;
        }

        public async Task<IEnumerable<ValidationErrorDTO>> Validate(SampleDTO dto, string code)
        {
            var errors = await validatorCli.Invoke(dto.CodeLanguage.ToString().ToLower(), dto.ValidatorKey, dto.Dependencies, code);
            return errors.Select(e => ValidationErrorDTO.Create(e));
        }
    }
}