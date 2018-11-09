namespace System.Xml.XPath
{
    public static class XPathExpressionExtensions
    {
        public static string GetControlName(this XPathExpression xpath)
        {
            var segments = xpath.Expression.Split('/');
            if (segments.Length > 0)
            {
                var last = segments[segments.Length - 1];
                if (!last.StartsWith("@") && last.Length > 0)
                {
                    return last;
                }
            }

            return null;
        }

        public static string GetDirectiveName(this XPathExpression xpath)
        {
            var segments = xpath.Expression.Split('/');
            if (segments.Length >= 2 && segments[1].StartsWith("@") && segments[1].Length > 1)
            {
                return segments[1].Substring(1);
            }

            return null;
        }

        public static string GetLastSegment(this XPathExpression xpath)
        {
            var segments = xpath.Expression.Split('/');
            if (segments.Length > 0)
            {
                return segments[segments.Length - 1];
            }

            return null;
        }

        public static XPathExpression GetLogicalParent(this XPathExpression xpath)
        {
            var lastSeparator = xpath.Expression.LastIndexOf('/');
            if (lastSeparator == -1)
            {
                return null;
            }

            var source = xpath.Expression.Substring(0, lastSeparator);
            if (string.IsNullOrEmpty(source))
            {
                return null;
            }
            if (source.EndsWith("/"))
            {
                source = source.Substring(0, source.Length - 1);
            }

            return XPathExpression.Compile(source);
        }

        public static string GetPropertyName(this XPathExpression xpath)
        {
            var segments = xpath.Expression.Split('/');
            if (segments.Length > 0)
            {
                var last = segments[segments.Length - 1];
                if (last.StartsWith("@") && last.Length > 1)
                {
                    return last.Substring(1);
                }
            }

            return null;
        }
    }
}