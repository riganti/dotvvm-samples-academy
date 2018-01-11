using AutoMapper;
using DotvvmAcademy.BL.Dtos;
using DotvvmAcademy.DAL.Entities;

namespace DotvvmAcademy.BL.Profiles
{
    public class StepPartDtoProfile : Profile
    {
        public StepPartDtoProfile()
        {
            CreateMap<IStepPart, IStepPartDto>();

            CreateMap<ExerciseBase, ExerciseStepPartDto>();

            CreateMap<HtmlStepPart, HtmlStepPartDto>()
                .IncludeBase<IStepPart, IStepPartDto>();

            CreateMap<CSharpExerciseStepPart, CSharpExerciseStepPartDto>()
                .IncludeBase<IStepPart, IStepPartDto>()
                .IncludeBase<ExerciseBase, ExerciseStepPartDto>();

            CreateMap<DothtmlExerciseStepPart, DothtmlExerciseStepPartDto>()
                .IncludeBase<IStepPart, IStepPartDto>()
                .IncludeBase<ExerciseBase, ExerciseStepPartDto>();
        }
    }
}