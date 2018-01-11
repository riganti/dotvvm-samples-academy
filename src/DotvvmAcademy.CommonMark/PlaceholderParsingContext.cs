using DotvvmAcademy.CommonMark.Segments;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DotvvmAcademy.CommonMark
{
    public class PlaceholderParsingContext : IPlaceholderParsingContext
    {
        private readonly ICollection<ISegment> segments;
        private readonly StringWriter output;

        internal PlaceholderParsingContext(string placeholder, ICollection<ISegment> segments, StringWriter output)
        {
            Placeholder = placeholder;
            this.segments = segments;
            this.output = output;
        }

        public string Placeholder { get; }

        public void AddSegment(ISegment segment) => segments.Add(segment);

        public Task WriteAsync(string text) => output.WriteAsync(text);
    }
}