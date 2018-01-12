using System.Collections.Generic;

namespace DotvvmAcademy.Validation
{
    public interface IValidationRequest<TItem, TRequirement>
        where TItem : IValidationItem
        where TRequirement : IValidationRequirement
    {
        IList<TItem> Items { get; }

        IList<TRequirement> Requirements{ get; }
    }
}