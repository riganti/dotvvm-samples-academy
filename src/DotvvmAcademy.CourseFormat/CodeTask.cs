namespace DotvvmAcademy.CourseFormat
{
    public class CodeTask : Source
    {
        public CodeTask(string path, string script) : base(path)
        {
            Script = script;
            CodeLanguage = SourcePath.GetCodeLanguage(path);
        }

        public string CodeLanguage { get; }

        public string Script { get; }
    }
}