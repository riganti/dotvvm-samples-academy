using AutoMapper;
using DotvvmAcademy.CommonMark.Components;
using DotvvmAcademy.DAL.Components;
using DotvvmAcademy.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.DAL.Profiles
{
    public class StepPartProfile : Profile
    {
        public StepPartProfile()
        {
            CreateMap<ICommonMarkComponent, IStepPart>();

            CreateMap<HtmlLiteralComponent, HtmlStepPart>()
                .IncludeBase<ICommonMarkComponent, IStepPart>();

            CreateMap<ExerciseComponentBase, ExerciseStepPartBase>()
                .IncludeBase<ICommonMarkComponent, IStepPart>()
                .ForMember(d => d.CorrectPath, ex => ex.MapFrom(s => s.Correct))
                .ForMember(d => d.IncorrectPath, ex => ex.MapFrom(s => s.Incorrect));

            CreateMap<CSharpExerciseComponent, CSharpExerciseStepPart>()
                .IncludeBase<ExerciseComponentBase, ExerciseStepPartBase>()
                .ForMember(d => d.DependencyPaths, ex => ex.MapFrom(s => s.Dependencies));

            CreateMap<DothtmlExerciseComponent, DothtmlExerciseStepPart>()
                .IncludeBase<ExerciseComponentBase, ExerciseStepPartBase>()
                .ForMember(d => d.MasterPagePath, ex => ex.MapFrom(s => s.MasterPage))
                .ForMember(d => d.ViewModelPath, ex => ex.MapFrom(s => s.ViewModel));

            CreateMap<MvvmExerciseComponent, MvvmExerciseStepPart>()
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
