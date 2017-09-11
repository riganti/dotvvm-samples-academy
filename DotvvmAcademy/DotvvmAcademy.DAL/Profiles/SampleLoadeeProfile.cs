using AutoMapper;
using DotvvmAcademy.DAL.Entities;
using DotvvmAcademy.DAL.Loadees;

namespace DotvvmAcademy.DAL.Profiles
{
    public class SampleLoadeeProfile : Profile
    {
        public SampleLoadeeProfile()
        {
            CreateMap<SampleLoadee, Sample>()
                .ForMember(d => d.Path, ex => ex.MapFrom(s => s.File));
        }
    }
}