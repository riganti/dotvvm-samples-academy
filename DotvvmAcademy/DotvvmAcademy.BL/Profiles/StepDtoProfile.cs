using AutoMapper;
using DotvvmAcademy.BL.Dtos;
using DotvvmAcademy.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
