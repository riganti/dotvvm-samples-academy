using Microsoft.CodeAnalysis;
using System;
using System.IO;

namespace DotvvmAcademy.CourseFormat
{
    public class CodeTaskSourceResolver : SourceReferenceResolver
    {
        private readonly CourseWorkspace workspace;

        public CodeTaskSourceResolver(CourseWorkspace workspace)
        {
            this.workspace = workspace;
        }

        public override bool Equals(object other) => throw new NotImplementedException();

        public override int GetHashCode() => throw new NotImplementedException();

        public override string NormalizePath(string path, string baseFilePath) => path;

        public override Stream OpenRead(string resolvedPath)
        {
            var file = workspace.GetFile(resolvedPath);
            if (file.Exists)
            {
                return file.OpenRead();
            }

            return Stream.Null;
        }

        public override string ResolveReference(string path, string baseFilePath)
        {
            var parent = SourcePath.GetParent(baseFilePath);
            return SourcePath.Normalize(SourcePath.Combine(parent, path));
        }
    }
}