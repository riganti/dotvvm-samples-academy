using System;
using System.IO;

namespace DotvvmAcademy.CourseFormat
{
    public class Archive : Source, IDisposable
    {
        public Archive(string path, Stream stream) : base(path)
        {
            Stream = stream;
        }

        public Stream Stream { get; }

        public void Dispose()
        {
            Stream.Dispose();
        }

        public override long GetSize()
        {
            return Stream.Length;
        }
    }
}