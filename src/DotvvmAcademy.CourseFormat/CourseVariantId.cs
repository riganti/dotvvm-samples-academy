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
        }

        public string Moniker { get; }

        public string Path { get; }
    }
}