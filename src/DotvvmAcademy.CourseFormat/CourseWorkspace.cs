using System.Collections.Immutable;
using System.IO;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public class CourseWorkspace
    {
        private ImmutableDictionary<string, Task<Source>> sources;

        public CourseWorkspace(string root) : this(new DirectoryInfo(root))
        {
        }

        public CourseWorkspace(DirectoryInfo root)
        {
            Root = root;
            Refresh();
        }

        public DirectoryInfo Root { get; }

        public DirectoryInfo GetDirectory(string virtualPath)
        {
            return new DirectoryInfo(Path.Combine(Root.FullName, $".{virtualPath}"));
        }

        public FileInfo GetFile(string sourcePath)
        {
            return new FileInfo(Path.Combine(Root.FullName, $".{sourcePath}"));
        }

        public Task<Source> Load(string path)
        {
            if (sources.TryGetValue(path, out var source))
            {
                return source;
            }

            return Task.FromResult<Source>(null);
        }

        public async Task<TSource> Load<TSource>(string path)
            where TSource : Source
        {
            var source = await Load(path);
            if (source == null)
            {
                return null;
            }

            if (source is TSource cast)
            {
                return cast;
            }
            else
            {
                return null;
            }
        }

        public Task<CodeTask> LoadCodeTask(string variant, string lesson, string step, string codeTask)
            => Load<CodeTask>($"/{SourceVisitor.ContentDirectory}/{variant}/{lesson}/{step}/{codeTask}");

        public Task<Lesson> LoadLesson(string variant, string lesson)
            => Load<Lesson>($"/{SourceVisitor.ContentDirectory}/{variant}/{lesson}");

        public Task<Step> LoadStep(string variant, string lesson, string step)
            => Load<Step>($"/{SourceVisitor.ContentDirectory}/{variant}/{lesson}/{step}");

        public Task<CourseVariant> LoadVariant(string variant)
            => Load<CourseVariant>($"/{SourceVisitor.ContentDirectory}/{variant}");

        public void Refresh() => sources = new SourceVisitor().Visit(Root);
    }
}