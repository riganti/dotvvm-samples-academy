using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Text;

namespace DotvvmAcademy.CourseFormat
{
    internal class CourseVariant : ICourseVariant
    {
        public CourseVariant(CourseVariantId id)
        {
            Id = id;
        }

        public string Annotation { get; set; }

        public CourseVariantId Id { get; }

        public ImmutableArray<LessonId> Lessons { get; set; }
    }
}
