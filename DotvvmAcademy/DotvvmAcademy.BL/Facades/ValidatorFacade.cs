using DotvvmAcademy.BL.DTO;
using DotvvmAcademy.BL.Services;
using System;
using System.Collections.Generic;

namespace DotvvmAcademy.BL.Facades
{
    public sealed class ValidatorFacade
    {
        private readonly ValidatorCli cli;

        public ValidatorFacade(ValidatorCli cli)
        {
            this.cli = cli;
        }

        public IEnumerable<ValidationErrorDTO> Validate(SampleDTO dto, string code)
        {
            // TODO: use the validatorCli
            throw new NotImplementedException();
        }
    }
}