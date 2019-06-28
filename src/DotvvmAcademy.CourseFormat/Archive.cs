namespace DotvvmAcademy.CourseFormat
{
    public class Archive
    {
        public Archive(string path, string name)
        {
            Path = path;
            Name = name;
        }

        public string Path { get; }

        public string Name { get; }
    }
}