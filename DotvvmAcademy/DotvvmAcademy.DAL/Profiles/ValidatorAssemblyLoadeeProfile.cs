﻿using AutoMapper;
using DotvvmAcademy.DAL.Entities;
using DotvvmAcademy.DAL.Loadees;

namespace DotvvmAcademy.DAL.Profiles
{
    public class ValidatorAssemblyLoadeeProfile : Profile
    {
        public ValidatorAssemblyLoadeeProfile()
        {
            CreateMap<ValidatorAssemblyLoadee, ValidatorAssembly>()
                .ForMember(d => d.Path, ex => ex.MapFrom(s => s.Dll))
                .ForMember(d => d.AbsolutePath, ex => ex.MapFrom(s => s.Dll.FullName));
        }
    }
}