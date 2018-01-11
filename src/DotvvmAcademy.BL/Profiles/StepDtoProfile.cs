using AutoMapper;
using DotvvmAcademy.BL.Dtos;
using DotvvmAcademy.DAL.Entities;

namespace DotvvmAcademy.BL.Profiles
{
    public class StepDtoProfile : Profile
    {
        public StepDtoProfile()
        {
            CreateMap<Step, StepDto>();
        }
    }
}