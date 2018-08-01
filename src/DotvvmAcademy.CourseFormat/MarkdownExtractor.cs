using Markdig;
using Markdig.Renderers;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using System.IO;

namespace DotvvmAcademy.CourseFormat
{
    public class MarkdownExtractor
    {
        public MarkdownLessonInfo Extract(Lesson lesson)
        {
            var document = Markdown.Parse(lesson.Annotation ?? string.Empty);
            var name = ExtractName(document);
            var imageUrl = ExtractImageUrl(document);
            var html = ExtractHtml(document);
            return new MarkdownLessonInfo(lesson, html, imageUrl, name);
        }

        private string ExtractHtml(MarkdownDocument document)
        {
            using (var writer = new StringWriter())
            {
                var renderer = new HtmlRenderer(writer);
                renderer.Render(document);
                return writer.ToString();
            }
        }

        private string ExtractImageUrl(MarkdownDocument document)
        {
            if (document.Count > 0
                && document[0] is ParagraphBlock paragraph
                && paragraph.Inline != null
                && paragraph.Inline.FirstChild != null
                && paragraph.Inline.FirstChild is LinkInline link)
            {
                document.Remove(document[0]);
                return link.Url;
            }

            return null;
        }

        private string ExtractName(MarkdownDocument document)
        {
            if (document.Count > 0
                && document[0] is HeadingBlock heading
                && heading.Inline != null
                && heading.Inline.FirstChild != null
                && heading.Inline.FirstChild is LiteralInline literal)
            {
                document.Remove(document[0]);
                return literal.ToString();
            }

            return null;
        }
    }
}