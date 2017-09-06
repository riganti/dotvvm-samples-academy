using AutoMapper;
using DotvvmAcademy.BL.Dtos;
using DotvvmAcademy.DAL.Entities;
using DotvvmAcademy.DAL.Providers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.BL.Facades
{
    public class LessonFacade : IFacade
    {
        private readonly LessonProvider provider;

        public LessonFacade(LessonProvider provider)
        {
            this.provider = provider;
        }

        public async Task<LessonOverviewDto> GetOverview(string moniker, string language)
        {
            var lesson = await provider.Get(moniker, language);
            if (lesson.IsReady)
            {
                return Mapper.Map<Lesson, LessonOverviewDto>(lesson);
            }
            return null;
        }

        public async Task<IEnumerable<LessonOverviewDto>> GetOverviews(string language)
        {
            var lessons = await provider.GetMany(language: language);
            return lessons.Where(l => l.IsReady).Select(l => Mapper.Map<Lesson, LessonOverviewDto>(l));
        }
    }
}