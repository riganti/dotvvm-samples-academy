using DotvvmAcademy.BL.DTO;
using DotvvmAcademy.DAL.Base.Providers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.BL.Facades
{
    public class SampleFacade
    {
        private ISampleProvider sampleProvider;
        private ILessonProvider lessonProvider;

        public SampleFacade(ILessonProvider lessonProvider, ISampleProvider sampleProvider)
        {
            this.lessonProvider = lessonProvider;
            this.sampleProvider = sampleProvider;
        }

        public SampleDTO GetSample(int lessonIndex, string language, int stepIndex, string correctPath, string incorrectPath)
        {
            var correctPathLanguage = GetSampleLanguage(correctPath);
            var incorrectPathLanguage = GetSampleLanguage(incorrectPath);
            if (correctPathLanguage != incorrectPathLanguage)
            {
                throw new InvalidOperationException($"The samples '{correctPath}' and '{incorrectPath}' do not share the same programming language.");
            }

            var sample = new SampleDTO(lessonIndex, language, stepIndex)
            {
                CodeLanguage = correctPathLanguage,
                CorrectCode = GetSample(lessonIndex, language, stepIndex, correctPath),
                IncorrectCode = GetSample(lessonIndex, language, stepIndex, incorrectPath)
            };

            return sample;
        }

        public string GetSample(int lessonIndex, string lessonLanguage, int stepIndex, string path)
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
                    return SampleCodeLanguage.Html;
                default:
                    throw new NotSupportedException($"The sample '{path}' has an unrecognized file extension.");
            }
        }
    }
}