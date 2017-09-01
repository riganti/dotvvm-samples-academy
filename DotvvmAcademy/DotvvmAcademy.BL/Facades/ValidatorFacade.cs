using DotvvmAcademy.BL.DTO;
using DotvvmAcademy.Validation.Cli.Host;
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

        public async Task<IEnumerable<ValidationErrorDTO>> Validate(SampleDTO dto, string validatorAssemblyPath, string code)
        {
            var arguments = new ValidatorCliArguments(dto.CodeLanguage.ToString().ToLower(), dto.ValidatorKey, validatorAssemblyPath)
            {
                Dependencies = dto.Dependencies.ToList()
            };
            var errors = await validatorCli.Invoke(arguments, code);
            return errors.Select(e => ValidationErrorDTO.Create(e));
        }
    }
}