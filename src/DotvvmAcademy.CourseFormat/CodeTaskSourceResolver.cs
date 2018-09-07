using Microsoft.CodeAnalysis;
using System;
using System.IO;

namespace DotvvmAcademy.CourseFormat
{
    public class CodeTaskSourceResolver : SourceReferenceResolver
    {
        private readonly CourseEnvironment environment;

        public CodeTaskSourceResolver(CourseEnvironment environment)
        {
            this.environment = environment;
        }

        public override bool Equals(object other)
        {
            throw new NotSupportedException();
        }

        public override int GetHashCode()
        {
            throw new NotSupportedException();
        }

        public override string NormalizePath(string path, string baseFilePath)
        {
            return path;
        }

        public override Stream OpenRead(string resolvedPath)
        {
            var file = environment.GetFile(resolvedPath);
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