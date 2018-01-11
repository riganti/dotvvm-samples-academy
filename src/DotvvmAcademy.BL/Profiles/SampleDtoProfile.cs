using AutoMapper;
using DotvvmAcademy.BL.Dtos;
using DotvvmAcademy.DAL.Entities;

namespace DotvvmAcademy.BL.Profiles
{
    public class SampleDtoProfile : Profile
    {
        public SampleDtoProfile()
        {
            CreateMap<Sample, SampleDto>();
        }
    }
}