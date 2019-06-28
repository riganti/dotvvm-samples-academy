using System;

namespace DotvvmAcademy.Validation
{
    public interface IConstraint
    {
        void Validate(IServiceProvider services);
    }
}