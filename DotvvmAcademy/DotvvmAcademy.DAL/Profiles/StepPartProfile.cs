using AutoMapper;
using DotvvmAcademy.CommonMark.Segments;
using DotvvmAcademy.DAL.Entities;
using DotvvmAcademy.DAL.Loadees;

namespace DotvvmAcademy.DAL.Profiles
{
    public class StepPartProfile : Profile
    {
        public StepPartProfile()
        {
            CreateMap<ISegment, IStepPart>();

            CreateMap<HtmlSegment, HtmlStepPart>()
                .IncludeBase<ISegment, IStepPart>();

            CreateMap<ExerciseLoadeeBase, ExerciseStepPartBase>()
                .ForMember(d => d.CorrectPath, ex => ex.MapFrom(s => s.Correct))
                .ForMember(d => d.IncorrectPath, ex => ex.MapFrom(s => s.Incorrect));

            CreateMap<CSharpExerciseSegment, CSharpExerciseStepPart>()
                .IncludeBase<ExerciseLoadeeBase, ExerciseStepPartBase>()
                .ForMember(d => d.DependencyPaths, ex => ex.MapFrom(s => s.Dependencies));

            CreateMap<DothtmlExerciseSegment, DothtmlExerciseStepPart>()
                .IncludeBase<ExerciseLoadeeBase, ExerciseStepPartBase>()
                .ForMember(d => d.MasterPagePath, ex => ex.MapFrom(s => s.MasterPage))
                .ForMember(d => d.ViewModelPath, ex => ex.MapFrom(s => s.ViewModel));

            CreateMap<MvvmExerciseSegment, MvvmExerciseStepPart>()
                .ForMember(d => d.CorrectViewPath, ex => ex.MapFrom(s => s.View.Correct))
                .ForMember(d => d.IncorrectViewPath, ex => ex.MapFrom(s => s.View.Incorrect))
                .ForMember(d => d.ViewValidatorId, ex => ex.MapFrom(s => s.View.ValidatorId))
                .ForMember(d => d.MasterPagePath, ex => ex.MapFrom(s => s.View.MasterPage))
                .ForMember(d => d.CorrectViewModelPath, ex => ex.MapFrom(s => s.ViewModel.Correct))
                .ForMember(d => d.IncorrectViewModelPath, ex => ex.MapFrom(s => s.ViewModel.Incorrect))
                .ForMember(d => d.ViewModelValidatorId, ex => ex.MapFrom(s => s.ViewModel.ValidatorId))
                .ForMember(d => d.ViewModelDependencies, ex => ex.MapFrom(s => s.ViewModel.Dependencies));
        }
    }
}