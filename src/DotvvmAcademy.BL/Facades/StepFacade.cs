using AutoMapper;
using DotvvmAcademy.BL.Dtos;
using DotvvmAcademy.DAL.Entities;
using DotvvmAcademy.DAL.Providers;
using System.Threading.Tasks;

namespace DotvvmAcademy.BL.Facades
{
    public class StepFacade : IFacade
    {
        private readonly LessonProvider lessonProvider;
        private readonly StepProvider stepProvider;

        public StepFacade(LessonProvider lessonProvider, StepProvider stepProvider)
        {
            this.lessonProvider = lessonProvider;
            this.stepProvider = stepProvider;
        }

        public async Task<StepDto> GetStep(LessonOverviewDto lessonDto, int index)
        {
            var lesson = await lessonProvider.Get(lessonDto.Moniker, lessonDto.Language);
            if(index >= lesson.StepPaths.Length || index < 0)
            {
                return null;
            }

            var path = lesson.StepPaths[index];
            var step = await stepProvider.Get(path);
            return Mapper.Map<Step, StepDto>(step);
        }
    }
}