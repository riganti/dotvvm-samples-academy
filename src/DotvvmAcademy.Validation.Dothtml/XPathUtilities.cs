using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.Dothtml
{
    public static class XPathUtilities
    {
        public const string Root = "/";

        public static bool IsTopLevel(string xpath)
        {
            return xpath.StartsWith(Root) && xpath.Split('/').Length == 2;
        }

        public static string GetParentPath(string xpath)
        {
            var lastSeparator = xpath.LastIndexOf('/');
            if (string.IsNullOrEmpty(xpath) || lastSeparator == -1)
            {
                return Root;
            }

            return xpath.Substring(0, lastSeparator);
        }

        public static string GetLastSegment(string xpath)
        {
            if (string.IsNullOrEmpty(xpath))
            {
                return null;
            }

            var index = xpath.LastIndexOf('/');
            return index == -1 ? xpath : xpath.Substring(index);
        }

        public static XPathKind GetKind(string xpath)
        {
            var segments = xpath.Split('/');
            var lastSegment = segments[^1];
            if (lastSegment.StartsWith("@"))
            {
                if (segments.Length == 1)
                {
                    return XPathKind.Directive;
                }
                else
                {
                    return XPathKind.Property;
                }
            }
            else if (lastSegment.Contains(':'))
            {
                return XPathKind.Control;
            }
            else
            {
                return XPathKind.Element;
            }
        }

        public static string GetName(string xpath)
        {
            var segment = GetLastSegment(xpath);
            if (string.IsNullOrEmpty(segment))
            {
                return segment;
            }
            int start = 0;
            if (segment[0] == '/')
            {
                start++;
            }
            if (segment.Length > 2 && segment[1] == '@')
            {
                start++;
            }
            int indexStart = segment.IndexOf('[');
            int end = indexStart == -1 ? segment.Length - 1 : indexStart - 1;
            return segment.Substring(start, end - start + 1);
        }
    }
}
