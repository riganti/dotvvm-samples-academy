using System.Collections.Immutable;

namespace DotvvmAcademy.CourseFormat
{
    public class WorkspaceRoot : Source
    {
        public WorkspaceRoot(
            ImmutableArray<string> variants,
            bool hasContent,
            bool hasResources)
            : base("/")
        {
            Variants = variants;
        }

        public bool HasContent { get; }

        public bool HasResources { get; }

        public ImmutableArray<string> Variants { get; }
    }
}