using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public class CourseFileProvider : ISourceProvider<CourseFile>
    {
        public const string MmfPrefix = nameof(CourseFile) + ":";
        private readonly ICourseEnvironment environment;

        public CourseFileProvider(ICourseEnvironment environment)
        {
            this.environment = environment;
        }

        public async Task<CourseFile> Get(string path)
        {
            var text = await environment.Read(path);
            using (var tempStream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(tempStream, text);
                tempStream.Position = 0;
                var mapName = $"{MmfPrefix}{path}";
                var mutex = new Mutex(true, $"Mutex-{mapName}", out var isMutexNew);
                if (!isMutexNew)
                {
                    mutex.WaitOne();
                }
                var mmf = MemoryMappedFile.CreateNew(mapName, tempStream.Length, MemoryMappedFileAccess.ReadWrite);
                using (var mmfStream = mmf.CreateViewStream(0, 0, MemoryMappedFileAccess.ReadWrite))
                {
                    tempStream.CopyTo(mmfStream);
                }
                mutex.ReleaseMutex();
                return new CourseFile(path, mmf, tempStream.Length, mapName, mutex);
            }
        }
    }
}