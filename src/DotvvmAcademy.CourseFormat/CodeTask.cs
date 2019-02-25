using System;
using System.IO.MemoryMappedFiles;
using System.Threading;

namespace DotvvmAcademy.CourseFormat
{
    public class CodeTask : Source, IDisposable
    {
        private readonly long size;

        public CodeTask(
            string path,
            MemoryMappedFile assembly,
            long size,
            string mapName,
            string entryTypeName,
            string entryMethodName,
            Mutex mutex)
            : base(path)
        {
            Assembly = assembly;
            MapName = mapName;
            this.size = size;
            EntryTypeName = entryTypeName;
            EntryMethodName = entryMethodName;
            Mutex = mutex;
        }

        public MemoryMappedFile Assembly { get; }

        public string EntryMethodName { get; }

        public string EntryTypeName { get; }

        public string MapName { get; }

        public Mutex Mutex { get; set; }

        public void Dispose()
        {
            Mutex.WaitOne();
            Assembly.Dispose();
            Mutex.Dispose();
        }

        public override long GetSize()
        {
            return size;
        }
    }
}