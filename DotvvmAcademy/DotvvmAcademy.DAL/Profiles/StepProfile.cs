using AutoMapper;
using DotvvmAcademy.DAL.Entities;
using DotvvmAcademy.DAL.Loadees;

namespace DotvvmAcademy.DAL.Profiles
{
    public class StepProfile : Profile
    {
        public StepProfile()
        {
            CreateMap<StepSource, Step>()
                .ForMember(d => d.Path, ex => ex.MapFrom(s => s.File))
                .ForMember(d => d.Source, ex => ex.MapFrom(s => s.Source));
        }
    }
}