using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    [DebuggerDisplay("CourseWorkspace: {RootDirectory}")]
    public class CourseWorkspace
    {

        public CourseWorkspace(string rootDirectory)
        {
            RootDirectory = new DirectoryInfo(rootDirectory);
            Refresh();
        }

        public ImmutableDictionary<string, CodeTaskId> CodeTaskIds { get; private set; }

        public ImmutableDictionary<string, LessonId> LessonIds { get; private set; }

        public ImmutableDictionary<string, StepId> StepIds { get; private set; }

        public ImmutableDictionary<string, CourseVariantId> VariantIds { get; private set; }

        public DirectoryInfo RootDirectory { get; }

        public void Refresh()
        {
            var visitor = new CourseWorkspaceVisitor();
            visitor.VisitRoot(RootDirectory);
            CodeTaskIds = visitor.CodeTasks.ToImmutableDictionary();
            LessonIds = visitor.Lessons.ToImmutableDictionary();
            StepIds = visitor.Steps.ToImmutableDictionary();
            VariantIds = visitor.Variants.ToImmutableDictionary();
        }

        public Task<ICodeTask> LoadCodeTask(string path)
        {
            return CodeTaskIds.TryGetValue(path, out var id) ? LoadCodeTask(id) : Task.FromResult<ICodeTask>(null);
        }

        public async Task<ICodeTask> LoadCodeTask(CodeTaskId id)
        {
            var codeTask = new CodeTask(id);
            codeTask.Code = await ReadFile(GetFile(id.CodePath).FullName);
            codeTask.ValidationScript = await ReadFile(GetFile(id.ValidationScriptPath).FullName);
            return codeTask;
        }

        public Task<ILesson> LoadLesson(string path)
        {
            return LessonIds.TryGetValue(path, out var id) ? LoadLesson(id) : Task.FromResult<ILesson>(null);
        }

        public async Task<ILesson> LoadLesson(LessonId id)
        {
            var dir = GetDirectory(id.Path);
            var lesson = new Lesson(id);
            var annotationFile = dir.EnumerateFiles("*.md", SearchOption.TopDirectoryOnly)
                .FirstOrDefault();
            if (annotationFile != null)
            {
                lesson.Annotation = await ReadFile(annotationFile.FullName);
            }
            lesson.Steps = dir.EnumerateDirectories()
                .Select(d => StepIds[$"{id.Path}/{d.Name}"])
                .ToImmutableDictionary(i => i.Path, i => i);
            return lesson;
        }

        public Task<IStep> LoadStep(string path)
        {
            return StepIds.TryGetValue(path, out var id) ? LoadStep(id) : Task.FromResult<IStep>(null);
        }

        public async Task<IStep> LoadStep(StepId id)
        {
            var dir = GetDirectory(id.Path);
            var step = new Step(id);
            var stepFile = dir.EnumerateFiles("*.md", SearchOption.TopDirectoryOnly)
                .FirstOrDefault();
            if (stepFile != null)
            {
                step.Text = await ReadFile(stepFile.FullName);
            }
            step.CodeTaskId = CodeTaskIds.GetValueOrDefault(id.Path);
            return step;
        }

        public Task<ICourseVariant> LoadVariant(string path)
        {
            return VariantIds.TryGetValue(path, out var id) ? LoadVariant(id) : Task.FromResult<ICourseVariant>(null);
        }

        public async Task<ICourseVariant> LoadVariant(CourseVariantId id)
        {
            var dir = GetDirectory(id.Path);
            var variant = new CourseVariant(id);
            var annotationFile = dir.EnumerateFiles("*.md", SearchOption.TopDirectoryOnly).FirstOrDefault();
            if (annotationFile != null)
            {
                variant.Annotation = await ReadFile(annotationFile.FullName);
            }
            variant.Lessons = dir.EnumerateDirectories()
                .Select(d => LessonIds[$"{id.Path}/{d.Name}"])
                .ToImmutableDictionary(i => i.Path, i => i);
            return variant;
        }

        public DirectoryInfo GetDirectory(string virtualPath)
        {
            return new DirectoryInfo(Path.Combine(RootDirectory.FullName, $".{virtualPath}"));
        }

        public FileInfo GetFile(string virtualPath)
        {
            return new FileInfo(Path.Combine(RootDirectory.FullName, $".{virtualPath}"));
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