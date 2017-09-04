using System.Collections.Generic;

namespace DotvvmAcademy.DAL.Base.Models
{
    public sealed class SampleFilter
    {
        public string Language { get; set; }

        public string LessonId { get; set; }

        public IEnumerable<string> Paths { get; set; }

        public int? StepIndex { get; set; }
    }
}