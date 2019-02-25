using System;
using System.IO.MemoryMappedFiles;

namespace DotvvmAcademy.CourseFormat
{
    public class CourseFile : Source, IDisposable
    {
        private readonly long size;

        public CourseFile(string path, MemoryMappedFile file, long size, string mapName) : base(path)
        {
            File = file;
            this.size = size;
            MapName = mapName;
        }

        public MemoryMappedFile File { get; }

        public string MapName { get; }

        public void Dispose()
        {
            File.Dispose();
        }

        public override long GetSize()
        {
            return size;
        }
    }
}