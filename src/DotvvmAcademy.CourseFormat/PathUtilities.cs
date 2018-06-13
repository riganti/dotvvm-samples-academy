using System.Text;

namespace DotvvmAcademy.CourseFormat
{
    internal static class PathUtilities
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