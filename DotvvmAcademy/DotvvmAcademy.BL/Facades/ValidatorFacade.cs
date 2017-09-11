using AutoMapper;
using DotvvmAcademy.BL.Dtos;
using DotvvmAcademy.DAL.Entities;
using DotvvmAcademy.DAL.Providers;
using DotvvmAcademy.Validation;
using DotvvmAcademy.Validation.Cli.Host;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.BL.Facades
{
    public class ValidatorFacade : IFacade
    {
        private readonly ValidatorCli cli;
        private readonly ValidatorAssemblyProvider provider;

        public ValidatorFacade(ValidatorCli cli, ValidatorAssemblyProvider provider)
        {
            this.cli = cli;
            this.provider = provider;
        }

        public async Task<IEnumerable<ValidationErrorDto>> Validate(ExerciseBaseDto exerciseDto, string code)
        {
            var codeLanguage = exerciseDto.CodeLanguage.ToString().ToLower();
            var validatorAssembly = await provider.Get();
            var args = new ValidatorCliArguments(codeLanguage, exerciseDto.ValidatorId, validatorAssembly.AbsolutePath);
            var errors = await cli.Invoke(args, code);
            return errors.Select(e => Mapper.Map<ValidationError, ValidationErrorDto>(e));
        }
    }
}