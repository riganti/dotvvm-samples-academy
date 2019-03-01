namespace DotvvmAcademy.CourseFormat
{
    public class Archive : Source
    {
        public Archive(string path, byte[] memory) : base(path)
        {
            Memory = memory;
        }

        public byte[] Memory { get; }
    }
}