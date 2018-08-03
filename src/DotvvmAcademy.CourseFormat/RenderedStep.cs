namespace DotvvmAcademy.CourseFormat
{
    public class RenderedStep : RenderedSource<Step>
    {
        public RenderedStep(Step source, string html, string codeTaskPath, string name) : base(source)
        {
            Html = html;
            CodeTaskPath = codeTaskPath;
            Name = name;
        }

        public string CodeTaskPath { get; }

        public string Html { get; }

        public string Name { get; }

        public override long GetSize()
        {
            return Html.Length + CodeTaskPath.Length + Name.Length;
        }
    }
}