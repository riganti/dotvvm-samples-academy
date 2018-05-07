using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    [DebuggerDisplay("CourseWorkspace[{Variants.Length}]: {RootDirectory}")]
    public class CourseWorkspace
    {
        private readonly ValidationService validationService = new ValidationService();

        public CourseWorkspace(string rootDirectory)
        {
            RootDirectory = new DirectoryInfo(rootDirectory).FullName;
            Variants = LoadVariantIds();
        }

        public string RootDirectory { get; }

        public ImmutableArray<CourseVariantId> Variants { get; }

        public string GetRelativePath(string absolutePath)
        {
            var absolute = new Uri(absolutePath, UriKind.Absolute);
            var root = new Uri(RootDirectory, UriKind.Absolute);
            return root.MakeRelativeUri(absolute).ToString();
        }

        public async Task<ICourseVariant> LoadVariant(CourseVariantId id)
        {
            var dir = new DirectoryInfo(Path.Combine(RootDirectory, id.Path));
            var variant = new CourseVariant(id);
            var annotationFile = dir.EnumerateFiles("*.md", SearchOption.TopDirectoryOnly)
                .FirstOrDefault();
            if (annotationFile != null)
            {
                variant.Annotation = await ReadFile(annotationFile.FullName);
            }
            variant.Lessons = dir.EnumerateDirectories()
                .Select(c => new LessonId(id, c.Name))
                .ToImmutableArray();
            return variant;
        }

        public async Task<ILesson> LoadLesson(LessonId id)
        {
            var dir = new DirectoryInfo(Path.Combine(RootDirectory, id.Path));
            var lesson = new Lesson(id);
            var annotationFile = dir.EnumerateFiles("*.md", SearchOption.TopDirectoryOnly)
                .FirstOrDefault();
            if (annotationFile != null)
            {
                lesson.Annotation = await ReadFile(annotationFile.FullName);
            }
            lesson.Steps = dir.EnumerateDirectories()
                .Select(c => new StepId(id, c.Name))
                .ToImmutableArray();
            return lesson;
        }

        public async Task<IStep> LoadStep(StepId id)
        {
            var dir = new DirectoryInfo(Path.Combine(RootDirectory, id.Path));
            var step = new Step(id);
            var stepFile = dir.EnumerateFiles("*.md", SearchOption.TopDirectoryOnly)
                .FirstOrDefault();
            if (stepFile != null)
            {
                step.Text = await ReadFile(stepFile.FullName);
            }
            var codeFile = dir.EnumerateFiles("code_*.*", SearchOption.TopDirectoryOnly).FirstOrDefault();
            var scriptFile = dir.EnumerateFiles("validate_*.csx", SearchOption.TopDirectoryOnly).FirstOrDefault();
            if (codeFile != null && scriptFile != null)
            {
                step.CodeTaskId = new CodeTaskId(id, codeFile.Name, scriptFile.Name);
            }
            return step;
        }

        public async Task<ICodeTask> LoadCodeTask(CodeTaskId id)
        {
            var codeTask = new CodeTask(id, validationService);
            codeTask.Code = await ReadFile(id.CodePath);
            codeTask.Language = Path.GetExtension(id.CodePath);
            return codeTask;
        }

        private ImmutableArray<CourseVariantId> LoadVariantIds()
        {
            return Directory.EnumerateDirectories(RootDirectory)
                .Select(c => new CourseVariantId(Path.GetFileName(c)))
                .ToImmutableArray();
        }

        private async Task<string> ReadFile(string path)
        {
            using (var reader = new StreamReader(path))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}