using DotVVM.Framework.Hosting;
using System;
using System.IO;

namespace DotvvmAcademy.Validation.Dothtml
{
    internal class ValidationHttpRequest : IHttpRequest
    {
        public ValidationHttpRequest(IHttpContext httpContext)
        {
            HttpContext = httpContext;
        }

        public Stream Body { get; }

        public string ContentType { get; }

        public ICookieCollection Cookies { get; }

        public IHeaderCollection Headers { get; }

        public IHttpContext HttpContext { get; }

        public bool IsHttps { get; }

        public string Method { get; }

        public IPathString Path { get; }

        public IPathString PathBase { get; }

        public IQueryCollection Query { get; }

        public string QueryString { get; }

        public string Scheme { get; }

        public Uri Url { get; }
    }
}