using System;
using System.Diagnostics;

namespace DotvvmAcademy.CourseFormat
{
    [DebuggerDisplay("LessonId: {Path}")]
    public sealed class LessonId
    {
        internal LessonId(CourseVariantId variantId, string moniker)
        {
            VariantId = variantId;
            Moniker = moniker;
            Path = $"{variantId.Path}/{moniker}";
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }

        public string Moniker { get; }

        public string Path { get; }

        public CourseVariantId VariantId { get; }
    }
}