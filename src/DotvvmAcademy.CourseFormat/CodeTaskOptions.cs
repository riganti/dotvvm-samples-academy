using System.Collections.Concurrent;

namespace DotvvmAcademy.CourseFormat
{
    public class CodeTaskOptions
    {
        public CodeTaskOptions(string scriptPath)
        {
            ScriptPath = scriptPath;
        }

        public string CorrectCodePath { get; set; }

        public string DefaultCodePath { get; set; }

        public string FileName { get; set; }

        public string ScriptPath { get; }

        public ConcurrentDictionary<string, string> SourcePaths { get; } = new ConcurrentDictionary<string, string>();
    }
}