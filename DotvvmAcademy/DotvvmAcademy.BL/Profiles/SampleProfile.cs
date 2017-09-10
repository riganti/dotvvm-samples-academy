using AutoMapper;
using DotvvmAcademy.BL.Dtos;
using DotvvmAcademy.DAL.Entities;

namespace DotvvmAcademy.BL.Profiles
{
    public class SampleProfile : Profile
    {
        public SampleProfile()
        {
            CreateMap<Sample, SampleDto>();
        }
    }
}