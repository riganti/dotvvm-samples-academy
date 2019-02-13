using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public class ArchiveProvider : ISourceProvider<Archive>
    {
        private readonly ICourseEnvironment environment;

        public ArchiveProvider(ICourseEnvironment environment)
        {
            this.environment = environment;
        }

        public async Task<Archive> Get(string path)
        {
            if (!await environment.Exists(path))
            {
                return null;
            }
            var memoryStream = new MemoryStream();
            using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                await AddRecursive(path, "", zipArchive);
            }
            return new Archive(path, memoryStream);
        }

        private async Task AddRecursive(string basePath, string path, ZipArchive archive)
        {
            foreach (var fileName in await environment.GetFiles(basePath))
            {
                var entry = archive.CreateEntry(Path.Combine(path, fileName));
                using (var inputStream = environment.OpenRead($"{basePath}/{fileName}"))
                using (var outputStream = entry.Open())
                {
                    await inputStream.CopyToAsync(outputStream);
                }
            }
            foreach (var directoryName in await environment.GetDirectories(basePath))
            {
                await AddRecursive($"{basePath}/{directoryName}", Path.Combine(path, directoryName), archive);
            }
        }
    }
}