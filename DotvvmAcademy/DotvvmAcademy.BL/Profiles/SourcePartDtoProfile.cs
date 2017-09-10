using AutoMapper;
using DotvvmAcademy.BL.Dtos;
using DotvvmAcademy.DAL.Entities;

namespace DotvvmAcademy.BL.Profiles
{
    public class SourcePartDtoProfile : Profile
    {
        public SourcePartDtoProfile()
        {
            CreateMap<IStepPart, IStepPartDto>();

            CreateMap<HtmlStepPart, HtmlStepPartDto>()
                .IncludeBase<IStepPart, IStepPartDto>();

            CreateMap<ExerciseStepPartBase, ExerciseDto>()
                .IncludeBase<IStepPart, IStepPartDto>()
                .ForMember(d => d.CodeLanguage, ex => ex.ResolveUsing(s => GetCodeLanguage(s.CorrectPath)));

            CreateMap<CSharpExerciseStepPart, CSharpExerciseStepPartDto>()
                .IncludeBase<ExerciseStepPartBase, ExerciseDto>();

            CreateMap<DothtmlExerciseStepPart, DothtmlExerciseStepPartDto>()
                .IncludeBase<ExerciseStepPartBase, ExerciseDto>();

            CreateMap<MvvmExerciseStepPart, MvvmExerciseStepPartDto>()
                .IncludeBase<IStepPart, IStepPartDto>()
                .ForMember(d => d.ViewExercise, ex => ex.ResolveUsing(part =>
                    {
                        var view = new ViewExerciseDto
                        {
                            CorrectPath = part.CorrectViewPath,
                            IncorrectPath = part.IncorrectViewPath,
                            ValidatorId = part.ViewValidatorId,
                            MasterPagePath = part.MasterPagePath,
                            CodeLanguage = GetCodeLanguage(part.CorrectViewPath)
                        };
                        return view;
                    }))
                .ForMember(d => d.ViewModelExercise, ex => ex.ResolveUsing(part =>
                    {
                        var viewModel = new ViewModelExerciseDto
                        {
                            CorrectPath = part.CorrectViewModelPath,
                            IncorrectPath = part.IncorrectViewModelPath,
                            ValidatorId = part.ViewModelValidatorId,
                            DependencyPaths = part.ViewModelDependencies,
                            CodeLanguage = GetCodeLanguage(part.CorrectViewModelPath)
                        };
                        return viewModel;
                    }));
        }

        private CodeLanguageDto GetCodeLanguage(string path)
        {
            var startIndex = path.LastIndexOf('.') + 1;
            var extension = path.Substring(startIndex, path.Length - startIndex);
            switch (extension)
            {
                case "cs": return CodeLanguageDto.CSharp;
                case "dothtml": return CodeLanguageDto.Dothtml;
                default: return CodeLanguageDto.Unknown;
            }
        }
    }
}