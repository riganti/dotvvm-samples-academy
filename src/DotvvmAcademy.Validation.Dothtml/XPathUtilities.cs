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
            var lastSegment = segments[segments.Length - 1];
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
            else if (segment[0] == '/')
            {
                if (segment.Length > 2 && segment[1] == '@')
                {
                    return segment.Substring(2);
                }
                else
                {
                    return segment.Substring(1);
                }
            }
            else
            {
                return segment;
            }
        }
    }
}
