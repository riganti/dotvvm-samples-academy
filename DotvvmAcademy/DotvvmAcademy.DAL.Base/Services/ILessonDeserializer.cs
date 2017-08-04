using DotvvmAcademy.DAL.Base.Entities;
using System.Collections.Generic;

namespace DotvvmAcademy.DAL.Base.Services
{
    public interface ILessonDeserializer
    {
        IEnumerable<Lesson> Deserialize(string rawFile);
    }
}