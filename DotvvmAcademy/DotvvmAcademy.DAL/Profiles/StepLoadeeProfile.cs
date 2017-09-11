using AutoMapper;
using DotvvmAcademy.DAL.Entities;
using DotvvmAcademy.DAL.Loadees;

namespace DotvvmAcademy.DAL.Profiles
{
    public class StepLoadeeProfile : Profile
    {
        public StepLoadeeProfile()
        {
            CreateMap<StepLoadee, Step>()
                .ForMember(d => d.Path, ex => ex.MapFrom(s => s.File))
                .ForMember(d => d.Source, ex => ex.MapFrom(s => s.Segments));
        }
    }
}