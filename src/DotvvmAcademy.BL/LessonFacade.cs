using DotvvmAcademy.CourseFormat;
using Markdig;
using Markdig.Renderers;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.BL
{
    public class LessonFacade
    {
        private readonly CourseWorkspace workspace;

        public LessonFacade(CourseWorkspace workspace)
        {
            this.workspace = workspace;
        }

        public async Task<LessonDTO> GetLesson(string language, string moniker)
        {
            var lesson = await workspace.LoadLesson($"/{language}/{moniker}");
            return CreateDTO(lesson);
        }

        public async Task<IEnumerable<LessonDTO>> GetLessons(string language)
        {
            var variant = await workspace.LoadVariant($"/{language}");
            if (variant == null)
            {
                return null;
            }
            var lessons = new List<LessonDTO>();
            foreach (var lessonId in variant.Lessons.Values)
            {
                var lesson = await workspace.LoadLesson(lessonId);
                lessons.Add(CreateDTO(lesson));
            }
            lessons.Sort((one, two) => string.Compare(one.Moniker, two.Moniker));
            return lessons;
        }

        private LessonDTO CreateDTO(ILesson lesson)
        {
            var dto = new LessonDTO();
            dto.IsFinished = false;
            dto.Moniker = lesson.Id.Moniker;
            dto.Steps = lesson.Steps.Values.Select(v => v.Moniker).ToList();
            dto.Steps.Sort(string.Compare);
            if (dto.Steps.Count > 0)
            {
                dto.CurrentStep = dto.Steps[0];
            }
            var document = Markdown.Parse(lesson.Annotation);
            if (document.Count > 0
                && document[0] is HeadingBlock heading
                && heading.Inline != null
                && heading.Inline.FirstChild != null
                && heading.Inline.FirstChild is LiteralInline literal)
            {
                dto.Name = literal.ToString();
                document.Remove(document[0]);
            }
            if (document.Count > 0
                && document[0] is ParagraphBlock paragraph
                && paragraph.Inline != null
                && paragraph.Inline.FirstChild != null
                && paragraph.Inline.FirstChild is LinkInline link)
            {
                dto.ImageUrl = link.Url;
                document.Remove(document[0]);
            }
            using (var writer = new StringWriter())
            {
                var renderer = new HtmlRenderer(writer);
                renderer.Render(document);
                dto.Annotation = writer.ToString();
            }
            return dto;
        }
    }
}