using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace DotvvmAcademy.CourseFormat
{
    public static class SourcePath
    {
        public const char DirectorySeparator = '/';
        public const string RootDirectory = "/";

        public static string Combine(string one, string two)
        {
            if (string.IsNullOrWhiteSpace(two))
            {
                return one;
            }
            if (two[0] == DirectorySeparator)
            {
                return two;
            }
            return $"{one}{DirectorySeparator}{two}";
        }

        public static string FromSystem(DirectoryInfo root, FileSystemInfo info)
            => GetPath(root.FullName, info.FullName);

        public static string GetCodeLanguage(string path)
        {
            var segment = GetLastSegment(path);
            var match = Regex.Match(segment, @"\w+\.(\w+)\.csx");
            if (match.Groups.Count == 2)
            {
                return match.Groups[1].Value;
            }

            return null;
        }

        public static string[] GetSegments(string path)
        {
            return path.Split(DirectorySeparator);
        }

        public static string GetLastSegment(string path)
        {
            var index = path.LastIndexOf(DirectorySeparator);
            return path.Substring(index + 1);
        }

        public static string GetParent(string path)
        {
            var index = path.LastIndexOf(DirectorySeparator);
            if (index + 1 >= path.Length)
            {
                return RootDirectory;
            }

            return path.Substring(0, index);
        }

        public static string GetPath(string root, string systemPath)
        {
            return systemPath.Substring(root.Length).Replace('\\', DirectorySeparator);
        }

        public static string Normalize(string path)
        {
            path = path.Trim();
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }
            bool isRooted = path[0] == DirectorySeparator;
            var segments = path.Split(DirectorySeparator);

            // 0. empty segments
            for (int i = 0; i < segments.Length; i++)
            {
                if (segments[i] == "")
                {
                    segments[i] = null;
                }
            }

            // 1. "." segments in the middle of the path
            for (int i = 1; i < segments.Length; i++)
            {
                if (segments[i] == ".")
                {
                    segments[i] = null;
                }
            }

            // 2. "dir/.." segments
            int toRemove = -1;
            for (int i = 0; i < segments.Length; i++)
            {
                if (segments[i] == ".." && toRemove != -1)
                {
                    segments[i] = null;
                    segments[toRemove] = null;
                    toRemove = -1;
                }
                else if (segments[i] != ".")
                {
                    toRemove = i;
                }
            }

            // 3. "./.." at the beginning of the path
            if (segments[0] == ".")
            {
                for (int i = 1; i < segments.Length; i++)
                {
                    if (segments[i] == "..")
                    {
                        segments[0] = null;
                        break;
                    }
                }
            }
            var sb = new StringBuilder();
            if (isRooted)
            {
                sb.Append(DirectorySeparator);
            }
            bool isFirst = true;
            for (int i = 0; i < segments.Length; i++)
            {
                if (segments[i] != null)
                {
                    if (!isFirst)
                    {
                        sb.Append(DirectorySeparator);
                    }
                    isFirst = false;
                    sb.Append(segments[i]);
                }
            }
            return sb.ToString();
        }
    }
}