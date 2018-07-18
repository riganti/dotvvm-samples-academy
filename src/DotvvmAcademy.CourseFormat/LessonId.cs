using System;
using System.Diagnostics;

namespace DotvvmAcademy.CourseFormat
{
    [DebuggerDisplay("LessonId: {Path}")]
    public sealed class LessonId : IResourceId
    {
        internal LessonId(CourseVariantId variantId, string moniker)
        {
            VariantId = variantId;
            Moniker = moniker;
            Path = $"{variantId.Path}/{moniker}";
        }

        public string Moniker { get; }

        public string Path { get; }

        public CourseVariantId VariantId { get; }
    }
}