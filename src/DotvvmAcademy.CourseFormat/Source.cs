namespace DotvvmAcademy.CourseFormat
{
    public abstract class Source
    {
        public Source(string path)
        {
            Path = path;
        }

        public string Path { get; }

        public abstract long GetSize();
    }
}