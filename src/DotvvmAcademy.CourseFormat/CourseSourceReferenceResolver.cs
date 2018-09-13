using Microsoft.CodeAnalysis;
using System;
using System.IO;

namespace DotvvmAcademy.CourseFormat
{
    public class CourseSourceReferenceResolver : SourceReferenceResolver
    {
        private readonly ICourseEnvironment environment;

        public CourseSourceReferenceResolver(ICourseEnvironment environment)
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
            return environment.OpenRead(resolvedPath);
        }

        public override string ResolveReference(string path, string baseFilePath)
        {
            var parent = SourcePath.GetParent(baseFilePath);
            return SourcePath.Normalize(SourcePath.Combine(parent, path));
        }
    }
}