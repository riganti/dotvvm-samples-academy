using DotvvmAcademy.DAL.Base.Entities;

namespace DotvvmAcademy.DAL.Base.Providers
{
    public interface ISourcePartProvider<TSourcePart> : IEntityProvider<TSourcePart>
        where TSourcePart : ISourcePart, new()
    {
    }
}