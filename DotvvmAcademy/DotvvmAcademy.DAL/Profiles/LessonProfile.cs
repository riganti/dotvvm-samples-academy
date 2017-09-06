﻿using AutoMapper;
using DotvvmAcademy.DAL.Entities;
using DotvvmAcademy.DAL.Loadees;

namespace DotvvmAcademy.DAL.Profiles
{
    public class LessonProfile : Profile
    {
        public LessonProfile()
        {
            CreateMap<LessonConfig, Lesson>()
                .ForMember(d => d.Path, ex => ex.MapFrom(s => s.File))
                .ForMember(d => d.StepPaths, ex => ex.MapFrom(s => s.Steps));
        }
    }
}