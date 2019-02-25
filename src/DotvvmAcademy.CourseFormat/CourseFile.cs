using System;
using System.IO.MemoryMappedFiles;
using System.Threading;

namespace DotvvmAcademy.CourseFormat
{
    public class CourseFile : Source, IDisposable
    {
        private readonly long size;

        public CourseFile(string path, MemoryMappedFile file, long size, string mapName, Mutex mutex) : base(path)
        {
            File = file;
            this.size = size;
            MapName = mapName;
            Mutex = mutex;
        }

        public MemoryMappedFile File { get; }

        public string MapName { get; }

        public Mutex Mutex { get; set; }

        public void Dispose()
        {
            Mutex.WaitOne();
            File.Dispose();
            Mutex.Dispose();
        }

        public override long GetSize()
        {
            return size;
        }
    }
}