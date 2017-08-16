using DotvvmAcademy.BL.DTO;
using DotvvmAcademy.BL.DTO.Components;
using DotvvmAcademy.DAL.Base.Providers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.BL.Facades
{
    public class SampleFacade
    {
        private ILessonProvider lessonProvider;
        private ISampleProvider sampleProvider;
        private ValidatorFacade validatorFacade;

        public SampleFacade(ILessonProvider lessonProvider, ISampleProvider sampleProvider, ValidatorFacade validatorFacade) 
        {
            this.lessonProvider = lessonProvider;
            this.sampleProvider = sampleProvider;
            this.validatorFacade = validatorFacade;
        }

        public SampleDTO GetSample(SampleComponent component)
        {
            var correctPathLanguage = GetSampleLanguage(component.CorrectPath);
            var incorrectPathLanguage = GetSampleLanguage(component.IncorrectPath);
            if (correctPathLanguage != incorrectPathLanguage)
            {
                throw new InvalidOperationException($"The samples '{component.CorrectPath}' and '{component.IncorrectPath}' " +
                    "do not share the same programming language.");
            }

            var sample = new SampleDTO(component.LessonIndex, component.Language, component.StepIndex)
            {
                CodeLanguage = correctPathLanguage,
                CorrectCode = GetRawSample(component.LessonIndex, component.Language, component.StepIndex, component.CorrectPath),
                IncorrectCode = GetRawSample(component.LessonIndex, component.Language, component.StepIndex, component.IncorrectPath),
                Validate = validatorFacade.GetValidator(component.Validator, correctPathLanguage)
            };

            return sample;
        }

        public string GetRawSample(int lessonIndex, string lessonLanguage, int stepIndex, string path)
        {
            var lesson = lessonProvider.Get(lessonIndex, lessonLanguage);
            return sampleProvider.Get(lesson, stepIndex, path);
        }

        public IEnumerable<string> GetSamples(int lessonIndex, string lessonLanguage, int stepIndex, IEnumerable<string> paths)
        {
            var lesson = lessonProvider.Get(lessonIndex, lessonLanguage);
            return sampleProvider.GetQueryable(lesson, stepIndex, paths).ToList();
        }

        private SampleCodeLanguage GetSampleLanguage(string path)
        {
            string extension = path.Substring(path.LastIndexOf('.') + 1);
            switch (extension)
            {
                case "cs":
                    return SampleCodeLanguage.CSharp;
                case "dothtml":
                    return SampleCodeLanguage.Dothtml;
                default:
                    throw new NotSupportedException($"The sample '{path}' has an unrecognized file extension.");
            }
        }
    }
}