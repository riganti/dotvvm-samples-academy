using AutoMapper;
using DotvvmAcademy.CommonMark.Segments;
using DotvvmAcademy.DAL.Entities;
using DotvvmAcademy.DAL.Loadees;
using static DotvvmAcademy.DAL.Entities.MvvmExerciseStepPart;
using static DotvvmAcademy.DAL.Loadees.MvvmExerciseSegment;

namespace DotvvmAcademy.DAL.Profiles
{
    public class SegmentsProfile : Profile
    {
        public SegmentsProfile()
        {
            CreateMap<ISegment, IStepPart>();

            CreateMap<HtmlSegment, HtmlStepPart>()
                .IncludeBase<ISegment, IStepPart>();

            CreateMap<ExerciseLoadeeBase, ExerciseBase>()
                .ForMember(d => d.FinalPath, ex => ex.MapFrom(s => s.Final))
                .ForMember(d => d.InitialPath, ex => ex.MapFrom(s => s.Initial));

            CreateMap<CSharpExerciseSegment, CSharpExerciseStepPart>()
                .IncludeBase<ISegment, IStepPart>()
                .IncludeBase<ExerciseLoadeeBase, ExerciseBase>()
                .ForMember(d => d.DependencyPaths, ex => ex.MapFrom(s => s.Dependencies));

            CreateMap<DothtmlExerciseSegment, DothtmlExerciseStepPart>()
                .IncludeBase<ISegment, IStepPart>()
                .IncludeBase<ExerciseLoadeeBase, ExerciseBase>()
                .ForMember(d => d.MasterPagePath, ex => ex.MapFrom(s => s.MasterPage))
                .ForMember(d => d.ViewModelPath, ex => ex.MapFrom(s => s.ViewModel));

            CreateMap<ViewExerciseLoadee, ViewExercise>()
                .IncludeBase<ExerciseLoadeeBase, ExerciseBase>();

            CreateMap<ViewModelExerciseLoadee, ViewModelExercise>()
                .IncludeBase<ExerciseLoadeeBase, ExerciseBase>();

            CreateMap<MvvmExerciseSegment, MvvmExerciseStepPart>()
                .IncludeBase<ISegment, IStepPart>();
        }
    }
}