using System.Linq;

namespace DotvvmAcademy.DAL.Base.Providers
{
    public interface IProvider<TIdentifier, TFilter, TProvidee>
    {
        TProvidee Get(TIdentifier identifier);

        IQueryable<TIdentifier> GetQueryable(TFilter filter);
    }
}