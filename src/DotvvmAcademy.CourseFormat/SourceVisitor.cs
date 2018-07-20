using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public class SourceVisitor
    {
        public const string CodeTaskPattern = "*.csx";
        public const string ContentDirectory = "content";
        public const string LessonPattern = "*.md";
        public const string ResourcesDirectory = "resources";
        public const string StepPattern = "*.md";
        public const string VariantPattern = "*.md";

        private ImmutableDictionary<string, Task<Source>>.Builder builder;
        private DirectoryInfo root;

        public ImmutableDictionary<string, Task<Source>> Visit(DirectoryInfo root)
        {
            this.root = root;
            builder = ImmutableDictionary.CreateBuilder<string, Task<Source>>();
            VisitRoot(root);
            return builder.ToImmutable();
        }

        private async Task<Source> LoadCodeTask(FileInfo file)
        {
            var path = SourcePath.FromSystem(root, file);
            var script = await ReadFile(file);
            return new CodeTask(path, script);
        }

        private async Task<Source> LoadLesson(DirectoryInfo directory)
        {
            var path = SourcePath.FromSystem(root, directory);
            var annotationFile = directory.GetFiles(LessonPattern).SingleOrDefault();
            var annotationText = await ReadFile(annotationFile);
            var stepNames = directory.GetDirectories().Select(d => d.Name).ToImmutableArray();
            return new Lesson(path, annotationText, stepNames);
        }

        private async Task<Source> LoadResource(FileInfo file)
        {
            var path = SourcePath.FromSystem(root, file);
            var text = await ReadFile(file);
            return new Resource(path, text);
        }

        private async Task<Source> LoadStep(DirectoryInfo directory)
        {
            var path = SourcePath.FromSystem(root, directory);
            var stepFile = directory.GetFiles(StepPattern).SingleOrDefault();
            var stepText = await ReadFile(stepFile);
            var codeTask = directory.GetFiles(CodeTaskPattern).SingleOrDefault();
            return new Step(path, stepText, codeTask.Name);
        }

        private async Task<Source> LoadVariant(DirectoryInfo directory)
        {
            var path = SourcePath.FromSystem(root, directory);
            var annotationFile = directory.GetFiles(VariantPattern).SingleOrDefault();
            var annotationText = await ReadFile(annotationFile);
            var lessonNames = directory.GetDirectories().Select(d => d.Name).ToImmutableArray();
            return new CourseVariant(path, annotationText, lessonNames);
        }

        private async Task<string> ReadFile(FileInfo file)
        {
            using (var reader = new StreamReader(file.FullName))
            {
                return await reader.ReadToEndAsync();
            }
        }

        private void VisitContent(DirectoryInfo directory)
        {
            foreach (var subdirectory in directory.GetDirectories())
            {
                VisitVariant(subdirectory);
            }
        }

        private void VisitLesson(DirectoryInfo directory)
        {
            builder.Add(SourcePath.FromSystem(root, directory), LoadLesson(directory));
            foreach (var subdirectory in directory.GetDirectories())
            {
                VisitStep(subdirectory);
            }
        }

        private void VisitResources(DirectoryInfo directory)
        {
            foreach (var file in directory.GetFiles())
            {
                builder.Add(SourcePath.FromSystem(root, file), LoadResource(file));
            }

            foreach (var subdirectory in directory.GetDirectories())
            {
                VisitResources(subdirectory);
            }
        }

        private void VisitRoot(DirectoryInfo directory)
        {
            var resources = new DirectoryInfo($"{directory.FullName}/{ResourcesDirectory}");
            VisitResources(resources);

            var content = new DirectoryInfo($"{directory.FullName}/{ContentDirectory}");
            VisitContent(content);
        }

        private void VisitStep(DirectoryInfo directory)
        {
            var path = SourcePath.FromSystem(root, directory);
            builder.Add(path, LoadStep(directory));
            var codeTask = directory.GetFiles("*.csx").SingleOrDefault();
            builder.Add(path, LoadCodeTask(codeTask));
        }

        private void VisitVariant(DirectoryInfo directory)
        {
            builder.Add(SourcePath.FromSystem(root, directory), LoadVariant(directory));
            foreach (var subdirectory in directory.GetDirectories())
            {
                VisitLesson(subdirectory);
            }
        }
    }
}