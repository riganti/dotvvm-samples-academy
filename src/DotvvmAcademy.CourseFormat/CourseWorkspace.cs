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
        private ImmutableDictionary<string, CodeTaskId> codeTasks = ImmutableDictionary.Create<string, CodeTaskId>();
        private ImmutableDictionary<string, LessonId> lessons = ImmutableDictionary.Create<string, LessonId>();
        private ImmutableDictionary<string, StepId> steps = ImmutableDictionary.Create<string, StepId>();
        private ImmutableDictionary<string, CourseVariantId> variants;

        public CourseWorkspace(string rootDirectory)
        {
            RootDirectory = new DirectoryInfo(rootDirectory);
        }

        public DirectoryInfo RootDirectory { get; }

        public ImmutableDictionary<string, CourseVariantId> GetVariantIds()
        {
            if (variants != null)
            {
                return variants;
            }
            variants = RootDirectory.EnumerateDirectories().ToImmutableDictionary(d => $"/{d.Name}", d => new CourseVariantId(d.Name));
            return variants;
        }

        public Task<ICodeTask> LoadCodeTask(string path)
        {
            return codeTasks.TryGetValue(path, out var id) ? LoadCodeTask(id) : Task.FromResult<ICodeTask>(null);
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
            return lessons.TryGetValue(path, out var id) ? LoadLesson(id) : Task.FromResult<ILesson>(null);
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
                .Select(d => GetStepId(id, d.Name))
                .ToImmutableDictionary(i => i.Path, i => i);
            return lesson;
        }

        public Task<IStep> LoadStep(string path)
        {
            return steps.TryGetValue(path, out var id) ? LoadStep(id) : Task.FromResult<IStep>(null);
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
            var codeFile = dir.EnumerateFiles("code_*.*", SearchOption.TopDirectoryOnly).FirstOrDefault();
            var scriptFile = dir.EnumerateFiles("validate_*.csx", SearchOption.TopDirectoryOnly).FirstOrDefault();
            if (codeFile != null && scriptFile != null)
            {
                step.CodeTaskId = GetCodeTaskId(id, codeFile.Name, scriptFile.Name);
            }
            return step;
        }

        public Task<ICourseVariant> LoadVariant(string path)
        {
            return variants.TryGetValue(path, out var id) ? LoadVariant(id) : Task.FromResult<ICourseVariant>(null);
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
                .Select(d => GetLessonId(id, d.Name))
                .ToImmutableDictionary(i => i.Path, i => i);
            return variant;
        }

        private CodeTaskId GetCodeTaskId(StepId stepId, string codeFile, string validationScriptFile)
        {
            if (!codeTasks.TryGetValue(stepId.Path, out var id))
            {
                id = new CodeTaskId(stepId, codeFile, validationScriptFile);
                var builder = ImmutableDictionary.CreateBuilder<string, CodeTaskId>();
                builder.AddRange(codeTasks);
                builder.Add(stepId.Path, id);
                codeTasks = builder.ToImmutable();
            }
            return id;
        }

        private DirectoryInfo GetDirectory(string virtualPath)
        {
            return new DirectoryInfo(Path.Combine(RootDirectory.FullName, $".{virtualPath}"));
        }

        private FileInfo GetFile(string virtualPath)
        {
            return new FileInfo(Path.Combine(RootDirectory.FullName, $".{virtualPath}"));
        }

        private LessonId GetLessonId(CourseVariantId variantId, string moniker)
        {
            if (!lessons.TryGetValue($"{variantId.Path}/{moniker}", out var id))
            {
                id = new LessonId(variantId, moniker);
                var builder = ImmutableDictionary.CreateBuilder<string, LessonId>();
                builder.AddRange(lessons);
                builder.Add(id.Path, id);
                lessons = builder.ToImmutable();
            }
            return id;
        }

        private StepId GetStepId(LessonId lessonId, string moniker)
        {
            if (!steps.TryGetValue($"{lessonId.Path}/{moniker}", out var id))
            {
                id = new StepId(lessonId, moniker);
                var builder = ImmutableDictionary.CreateBuilder<string, StepId>();
                builder.AddRange(steps);
                builder.Add(id.Path, id);
                steps = builder.ToImmutable();
            }
            return id;
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