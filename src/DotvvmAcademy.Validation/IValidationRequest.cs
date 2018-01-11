using System.Collections.Generic;

namespace DotvvmAcademy.Validation
{
    public interface IValidationRequest
    {
        IList<IValidationItem> Items { get; }

        IList<IValidationRequirement> Requirements{ get; }
    }
}