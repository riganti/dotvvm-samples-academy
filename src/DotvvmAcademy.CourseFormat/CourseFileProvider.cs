using System.IO.MemoryMappedFiles;
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
            using (var envStream = environment.OpenRead(path))
            {
                var mapName = $"{MmfPrefix}{path}";
                var mmf = MemoryMappedFile.CreateNew(mapName, envStream.Length, MemoryMappedFileAccess.ReadWrite);
                using (var mmfStream = mmf.CreateViewStream(0, 0, MemoryMappedFileAccess.ReadWrite))
                {
                    await mmfStream.CopyToAsync(mmfStream);
                }
                return new CourseFile(path, mmf, envStream.Length, mapName);
            }
        }
    }
}