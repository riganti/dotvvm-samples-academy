namespace DotvvmAcademy.DAL.Base.Cache
{
    public interface ICache<TIdentifier, TIdentifyee>
    {
        void Add(TIdentifier identifier, TIdentifyee identifyee);

        TIdentifyee Get(TIdentifier identifier);
    }
}