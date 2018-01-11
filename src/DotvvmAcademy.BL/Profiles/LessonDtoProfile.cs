using AutoMapper;
using DotvvmAcademy.BL.Dtos;
using DotvvmAcademy.DAL.Entities;

namespace DotvvmAcademy.BL.Profiles
{
    public class LessonDtoProfile : Profile
    {
        public LessonDtoProfile()
        {
            CreateMap<Lesson, LessonOverviewDto>()
                .ForMember(d => d.StepCount, ex => ex.MapFrom(s => s.StepPaths.Length));
        }
    }
}