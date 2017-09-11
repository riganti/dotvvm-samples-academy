using AutoMapper;
using DotvvmAcademy.BL.Dtos;
using DotvvmAcademy.DAL.Entities;
using static DotvvmAcademy.BL.Dtos.MvvmExerciseStepPartDto;
using static DotvvmAcademy.DAL.Entities.MvvmExerciseStepPart;

namespace DotvvmAcademy.BL.Profiles
{
    public class StepPartDtoProfile : Profile
    {
        public StepPartDtoProfile()
        {
            CreateMap<IStepPart, IStepPartDto>();

            CreateMap<ExerciseBase, ExerciseBaseDto>();

            CreateMap<HtmlStepPart, HtmlStepPartDto>()
                .IncludeBase<IStepPart, IStepPartDto>();

            CreateMap<CSharpExerciseStepPart, CSharpExerciseStepPartDto>()
                .IncludeBase<IStepPart, IStepPartDto>()
                .IncludeBase<ExerciseBase, ExerciseBaseDto>();

            CreateMap<DothtmlExerciseStepPart, DothtmlExerciseStepPartDto>()
                .IncludeBase<IStepPart, IStepPartDto>()
                .IncludeBase<ExerciseBase, ExerciseBaseDto>();

            CreateMap<ViewExercise, ViewExerciseDto>()
                .IncludeBase<ExerciseBase, ExerciseBaseDto>();

            CreateMap<MvvmExerciseStepPart, MvvmExerciseStepPartDto>()
                .IncludeBase<IStepPart, IStepPartDto>()
                .ForMember(d => d.ViewExercise, ex => ex.MapFrom(s => s.View))
                .ForMember(d => d.ViewModelExercise, ex => ex.MapFrom(s => s.ViewModel));
        }
    }
}