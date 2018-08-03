using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public class SourceLoader
    {
        public const string CodeTaskPattern = "*.csx";
        public const string LessonPattern = "*.md";
        public const string StepPattern = "*.md";
        public const string VariantPattern = "*.md";

        private readonly CourseEnvironment environment;

        public SourceLoader(CourseEnvironment environment)
        {
            this.environment = environment;
        }

        public Task<Source> Load(string sourcePath)
        {
            if (!environment.Exists(sourcePath))
            {
                return Task.FromResult<Source>(null);
            }

            var segments = SourcePath.GetSegments(sourcePath);
            if (segments.Length <= 1)
            {
                return LoadRoot(environment.Root);
            }

            switch (segments[1])
            {
                case CourseEnvironment.ResourcesDirectory:
                    return LoadResource(environment.GetFile(sourcePath));

                case CourseEnvironment.ContentDirectory:
                    switch (segments.Length)
                    {
                        case 3: // /content/variant
                            return LoadVariant(environment.GetDirectory(sourcePath));

                        case 4: // /content/variant/lesson
                            return LoadLesson(environment.GetDirectory(sourcePath));

                        case 5: // /content/variant/lesson/step
                            return LoadStep(environment.GetDirectory(sourcePath));
                    }
                    break;
            }

            return Task.FromResult<Source>(null);
        }

        private async Task<Source> LoadLesson(DirectoryInfo directory)
        {
            var path = SourcePath.FromSystem(environment.Root, directory);
            var annotationFile = directory.GetFiles(LessonPattern).FirstOrDefault();
            var annotationText = await ReadFile(annotationFile);
            var stepNames = directory.GetDirectories().Select(d => d.Name).ToImmutableArray();
            return new Lesson(path, annotationText, stepNames);
        }

        private async Task<Source> LoadResource(FileInfo file)
        {
            var path = SourcePath.FromSystem(environment.Root, file);
            var text = await ReadFile(file);
            return new Resource(path, text);
        }

        private Task<Source> LoadRoot(DirectoryInfo directory)
        {
            if (directory == null || !directory.Exists)
            {
                return Task.FromResult<Source>(null);
            }

            var content = new DirectoryInfo($"{directory.FullName}/{CourseEnvironment.ContentDirectory}");
            var variants = content.GetDirectories().Select(d => d.Name).ToImmutableArray();
            var resources = new DirectoryInfo($"{directory.FullName}/{CourseEnvironment.ResourcesDirectory}");
            return Task.FromResult<Source>(new WorkspaceRoot(
                variants: variants,
                hasContent: content.Exists,
                hasResources: resources.Exists));
        }

        private async Task<Source> LoadStep(DirectoryInfo directory)
        {
            if (directory == null || !directory.Exists)
            {
                return null;
            }

            var path = SourcePath.FromSystem(environment.Root, directory);
            var stepFile = directory.GetFiles(StepPattern).FirstOrDefault();
            var stepText = await ReadFile(stepFile);
            return new Step(path, stepText);
        }

        private async Task<Source> LoadVariant(DirectoryInfo directory)
        {
            if (directory == null || !directory.Exists)
            {
                return null;
            }

            var path = SourcePath.FromSystem(environment.Root, directory);
            var annotationFile = directory.GetFiles(VariantPattern).FirstOrDefault();
            var annotationText = await ReadFile(annotationFile);
            var lessonNames = directory.GetDirectories().Select(d => d.Name).ToImmutableArray();
            return new CourseVariant(path, annotationText, lessonNames);
        }

        private async Task<string> ReadFile(FileInfo file)
        {
            if (file == null || !file.Exists)
            {
                return null;
            }

            using (var reader = new StreamReader(file.FullName))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}