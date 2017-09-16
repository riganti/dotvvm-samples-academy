using DotvvmAcademy.Lessons;
using System.Collections.Generic;

namespace DotvvmAcademy.Services
{
    public interface ILessonProvider
    {
        string Language { get; }

        Dictionary<int, LessonBase> CreateLessons();
    }
}