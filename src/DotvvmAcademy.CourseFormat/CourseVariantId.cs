using System;
using System.Diagnostics;

namespace DotvvmAcademy.CourseFormat
{
    [DebuggerDisplay("CourseVariantId: {Path}")]
    public sealed class CourseVariantId
    {
        internal CourseVariantId(string moniker)
        {
            Path = $"/{moniker}";
            Moniker = moniker;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }

        public string Moniker { get; }

        public string Path { get; }
    }
}