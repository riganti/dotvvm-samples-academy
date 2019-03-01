namespace DotvvmAcademy.CourseFormat
{
    public class CodeTask : Source
    {
        public CodeTask(string path, byte[] assembly, string entryTypeName, string entryMethodName)
            : base(path)
        {
            Assembly = assembly;
            EntryTypeName = entryTypeName;
            EntryMethodName = entryMethodName;
        }

        public byte[] Assembly { get; }

        public string EntryMethodName { get; }

        public string EntryTypeName { get; }
    }
}