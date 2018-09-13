using System;
using System.Collections.Immutable;

namespace DotvvmAcademy.CourseFormat
{
    public class Variant : Source
    {
        public Variant(string path, ImmutableArray<string> lessons) : base(path)
        {
            Lessons = lessons;
            Moniker = SourcePath.GetLastSegment(Path).ToString();
        }

        public ImmutableArray<string> Lessons { get; }

        public string Moniker { get; }

        public override long GetSize()
        {
            return 1;
        }
    }
}