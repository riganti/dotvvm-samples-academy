using DotvvmAcademy.BL.DTO.Components;
using System.Collections.Generic;

namespace DotvvmAcademy.BL.DTO
{
    public class StepDTO
    {
        public StepDTO(int lessonIndex, string language, int index)
        {
            LessonIndex = lessonIndex;
            Language = language;
            Index = index;
        }

        public int Index { get; }

        public string Language { get; }

        public int LessonIndex { get; }

        public List<SourceComponent> Source { get; internal set; }
    }
}