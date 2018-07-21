using System.Collections.Concurrent;

namespace DotvvmAcademy.CourseFormat
{
    public class SourcePathStorage
    {
        private readonly ConcurrentDictionary<string, string> paths = new ConcurrentDictionary<string, string>();

        public void Add(string key, string sourcePath)
        {
            paths.AddOrUpdate(key, sourcePath, (first, second) => second);
        }

        public string Get(string key)
        {
            if (paths.TryGetValue(key, out var sourcePath))
            {
                return sourcePath;
            }

            return null;
        }
    }
}