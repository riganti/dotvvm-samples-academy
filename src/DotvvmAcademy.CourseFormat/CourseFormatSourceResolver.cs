using Microsoft.CodeAnalysis;
using System;
using System.IO;

namespace DotvvmAcademy.CourseFormat
{
    internal class CourseFormatSourceResolver : SourceReferenceResolver
    {
        private readonly CourseWorkspace workspace;

        public CourseFormatSourceResolver(CourseWorkspace workspace)
        {
            this.workspace = workspace;
        }

        public override bool Equals(object other) => throw new NotImplementedException();

        public override int GetHashCode() => throw new NotImplementedException();

        public override string NormalizePath(string path, string baseFilePath) => path;

        public override Stream OpenRead(string resolvedPath)
        {
            if(workspace.CodeTaskIds.TryGetValue(resolvedPath, out var id))
            {
                return workspace.GetFile(id.ValidationScriptPath).OpenRead();
            }
            return null;
        }

        public override string ResolveReference(string path, string baseFilePath)
        {
            return PathUtilities.Normalize(PathUtilities.Combine(baseFilePath, path));
        }
    }
}