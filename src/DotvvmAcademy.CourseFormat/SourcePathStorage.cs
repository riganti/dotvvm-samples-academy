using System.Collections.Concurrent;

namespace DotvvmAcademy.CourseFormat
{
    public class SourcePathStorage
    {
        private readonly ValidationScriptRunner.Context context;
        private readonly ConcurrentDictionary<string, string> paths = new ConcurrentDictionary<string, string>();

        public SourcePathStorage(ValidationScriptRunner.Context context)
        {
            this.context = context;
        }

        public void Add(string key, string sourcePath)
        {
            var directory = SourcePath.GetParent(context.ScriptPath);
            var absolutePath = SourcePath.Normalize(SourcePath.Combine(directory, sourcePath));
            paths.AddOrUpdate(key, absolutePath, (k, v) => absolutePath);
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