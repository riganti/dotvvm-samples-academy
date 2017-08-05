using DotvvmAcademy.Lessons;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System;

namespace DotvvmAcademy.Services
{
    public class CsLessonProvider : ILessonProvider
    {
        private readonly IHostingEnvironment hostingEnvironment;

        public CsLessonProvider(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        public string Language => "cs";

        public Dictionary<int, LessonBase> CreateLessons()
        {
            var lessons = new Dictionary<int, LessonBase>
            {
                {1, new LessonCs1(hostingEnvironment)},
                {2, new LessonCs2(hostingEnvironment)},
                {3, new LessonCs3(hostingEnvironment)},
                {4, new LessonCs4(hostingEnvironment)}
            };
            return lessons;
        }
    }
}