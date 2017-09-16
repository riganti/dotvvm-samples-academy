using DotvvmAcademy.Lessons;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;

namespace DotvvmAcademy.Services
{
    public class RuLessonProvider : ILessonProvider
    {
        private readonly IHostingEnvironment hostingEnvironment;

        public RuLessonProvider(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }
        public string Language => "ru";

        public Dictionary<int, LessonBase> CreateLessons()
        {
            var lessons = new Dictionary<int, LessonBase>
            {
                {1, new LessonRu1(hostingEnvironment)},
                {2, new LessonRu2(hostingEnvironment)},
                {3, new LessonRu3(hostingEnvironment)},
                {4, new LessonRu4(hostingEnvironment)}
            };
            return lessons;
        }
    }
}