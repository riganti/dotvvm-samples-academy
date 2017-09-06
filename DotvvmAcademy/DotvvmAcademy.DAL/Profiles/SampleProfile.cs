using AutoMapper;
using DotvvmAcademy.DAL.Entities;
using DotvvmAcademy.DAL.Loadees;

namespace DotvvmAcademy.DAL.Profiles
{
    public class SampleProfile : Profile
    {
        public SampleProfile()
        {
            CreateMap<SampleSource, Sample>()
                .ForMember(d => d.Path, ex => ex.MapFrom(s => s.File));
        }
    }
}