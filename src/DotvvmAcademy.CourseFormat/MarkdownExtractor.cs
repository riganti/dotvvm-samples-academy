using Markdig;
using Markdig.Renderers;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using System.IO;

namespace DotvvmAcademy.CourseFormat
{
    public class MarkdownExtractor
    {
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

            return null;
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

            return null;
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

            return null;
        }
    }
}