using AutoMapper;
using DotvvmAcademy.BL.Dtos;
using DotvvmAcademy.DAL.Entities;
using DotvvmAcademy.DAL.Providers;
using System.Threading.Tasks;

namespace DotvvmAcademy.BL.Facades
{
    public class SampleFacade : IFacade
    {
        private readonly SampleProvider sampleProvider;

        public SampleFacade(SampleProvider sampleProvider)
        {
            this.sampleProvider = sampleProvider;
        }

        public async Task<SampleDto> GetSample(string path)
        {
            var sample = await sampleProvider.Get(path);
            return Mapper.Map<Sample, SampleDto>(sample);
        }
    }
}