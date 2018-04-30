using System.Collections.Immutable;

namespace DotvvmAcademy.CourseFormat.Models
{
    public interface ILesson
    {
        ImmutableArray<IStep> Steps { get; }
    }
}