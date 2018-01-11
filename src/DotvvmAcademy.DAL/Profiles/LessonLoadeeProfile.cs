using AutoMapper;
using DotvvmAcademy.DAL.Entities;
using DotvvmAcademy.DAL.Loadees;

namespace DotvvmAcademy.DAL.Profiles
{
    public class LessonLoadeeProfile : Profile
    {
        public LessonLoadeeProfile()
        {
            CreateMap<LessonConfigLoadee, Lesson>()
                .ForMember(d => d.Path, ex => ex.MapFrom(s => s.File))
                .ForMember(d => d.StepPaths, ex => ex.MapFrom(s => s.Steps));
        }
    }
}