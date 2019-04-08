using Microsoft.CodeAnalysis;
using System;
using System.IO;

namespace DotvvmAcademy.CourseFormat
{
    public class CourseSourceReferenceResolver : SourceReferenceResolver
    {
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
            return new FileInfo(resolvedPath).OpenRead();
        }

        public override string ResolveReference(string path, string baseFilePath)
        {
            var parent = Path.GetDirectoryName(baseFilePath);
            return Path.Combine(parent, path);
        }
    }
}