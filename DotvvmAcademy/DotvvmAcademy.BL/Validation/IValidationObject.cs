using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.BL.Validation
{
    public interface IValidationObject<TValidate> where TValidate : Validate
    {
        bool IsActive { get; }

        TValidate Validate { get; }

        void AddError(string message);
    }
}
