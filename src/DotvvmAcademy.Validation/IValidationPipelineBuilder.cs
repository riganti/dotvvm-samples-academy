using System;

namespace DotvvmAcademy.Validation
{
    public interface IValidationPipelineBuilder
    {
        IValidationPipelineBuilder Use(Func<ValidationDelegate, ValidationDelegate> middleware);

        ValidationDelegate Build();
    }
}