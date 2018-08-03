using Markdig;
using Markdig.Renderers;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using System.IO;

namespace DotvvmAcademy.CourseFormat
{
    public class MarkdownExtractor
    {
        public const string MissingCodeTaskPath = null;
        public const string MissingImageUrl = null;
        public const string MissingName = "{Missing lesson name}";
        public const string CodeTaskLiteral = "CodeTask";

        public string ExtractCodeTaskPath(MarkdownDocument document)
        {
            if(document.Count > 0
                && document.LastChild is ParagraphBlock paragraph
                && paragraph.Inline != null
                && paragraph.Inline.FirstChild != null
                && paragraph.Inline.FirstChild == paragraph.Inline.LastChild
                && paragraph.Inline.FirstChild is LinkInline link
                && link.FirstChild == link.LastChild
                && link.FirstChild.ToString() == CodeTaskLiteral)
            {
                document.Remove(document.LastChild);
                return link.Url;
            }

            return MissingCodeTaskPath;
        }

        public string ExtractHtml(MarkdownDocument document)
        {
            using (var writer = new StringWriter())
            {
                var renderer = new HtmlRenderer(writer);
                renderer.Render(document);
                return writer.ToString();
            }
        }

        public string ExtractImageUrl(MarkdownDocument document)
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

            return MissingImageUrl;
        }

        public string ExtractName(MarkdownDocument document)
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

            return MissingName;
        }
    }
}