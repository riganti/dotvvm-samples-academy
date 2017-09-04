using DotvvmAcademy.DAL.Base.Models;

namespace DotvvmAcademy.DAL.Base.Providers
{
    public interface ISampleProvider : IProvider<SampleIdentifier, SampleFilter, string>
    {
    }
}