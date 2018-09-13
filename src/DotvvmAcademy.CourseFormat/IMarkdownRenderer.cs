using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public interface IMarkdownRenderer
    {
        Task<string> Render(string source);

        Task<(string html, TFrontMatter frontMatter)> Render<TFrontMatter>(string source);
    }
}