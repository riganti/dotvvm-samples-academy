using DotvvmAcademy.Lessons;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;

namespace DotvvmAcademy.Services
{
    public class EnLessonProvider : ILessonProvider
    {
        private readonly IHostingEnvironment hostingEnvironment;

        public EnLessonProvider(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }
        public string Language => "en";

        public Dictionary<int, LessonBase> CreateLessons()
        {
            var lessons = new Dictionary<int, LessonBase>
            {
                {1, new Lesson1(hostingEnvironment)},
                {2, new Lesson2(hostingEnvironment)},
                {3, new Lesson3(hostingEnvironment)},
                {4, new Lesson4(hostingEnvironment)}
            };
            return lessons;
        }
    }
}