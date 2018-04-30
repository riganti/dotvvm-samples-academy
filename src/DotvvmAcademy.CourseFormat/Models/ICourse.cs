using System.Collections.Immutable;

namespace DotvvmAcademy.CourseFormat.Models
{
    public interface ICourse
    {
        public string Culture { get; set; }

        ImmutableArray<ILesson> Lessons { get; set; }
    }
}