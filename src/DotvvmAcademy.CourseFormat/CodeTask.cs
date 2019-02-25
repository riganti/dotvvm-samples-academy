using System;
using System.IO.MemoryMappedFiles;

namespace DotvvmAcademy.CourseFormat
{
    public class CodeTask : Source, IDisposable
    {
        private readonly long size;

        public CodeTask(string path, MemoryMappedFile assembly, long size, string mapName, string entryTypeName, string entryMethodName)
            : base(path)
        {
            Assembly = assembly;
            MapName = mapName;
            this.size = size;
            EntryTypeName = entryTypeName;
            EntryMethodName = entryMethodName;
        }

        public MemoryMappedFile Assembly { get; }

        public string EntryMethodName { get; }

        public string EntryTypeName { get; }

        public string MapName { get; }

        public void Dispose()
        {
            Assembly.Dispose();
        }

        public override long GetSize()
        {
            return size;
        }
    }
}