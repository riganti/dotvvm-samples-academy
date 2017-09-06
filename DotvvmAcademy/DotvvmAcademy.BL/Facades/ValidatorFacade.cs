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
        private ValidatorAssembly validatorAssembly;

        public ValidatorFacade(ValidatorCli cli, ValidatorAssemblyProvider provider)
        {
            this.cli = cli;
            this.provider = provider;
        }

        public async Task RebuildAssembly()
        {
            validatorAssembly = await provider.Get();
        }

        public async Task<IEnumerable<ValidationErrorDto>> Validate(ExerciseDto exerciseDto, string code)
        {
            var codeLanguage = exerciseDto.CodeLanguage.ToString().ToLower();
            var args = new ValidatorCliArguments(codeLanguage, exerciseDto.ValidatorId, validatorAssembly.AbsolutePath);
            var errors = await cli.Invoke(args, code);
            return errors.Select(e => Mapper.Map<ValidationError, ValidationErrorDto>(e));
        }
    }
}