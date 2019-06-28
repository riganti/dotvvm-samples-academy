using DotVVM.Framework.Hosting;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.Dothtml
{
    internal class ValidationHttpResponse : IHttpResponse
    {
        public ValidationHttpResponse(IHttpContext context, IHeaderCollection headers)
        {
            Context = context;
            Headers = headers;
        }

        public Stream Body { get; set; } = new MemoryStream();

        public string ContentType { get; set; }

        public IHttpContext Context { get; }

        public IHeaderCollection Headers { get; }

        public int StatusCode { get; set; }

        public void Write(string text)
        {
            var writer = new StreamWriter(Body)
            {
                AutoFlush = true
            };
            writer.Write(text);
        }

        public void Write(byte[] data)
        {
            Body.Write(data, 0, data.Length);
        }

        public void Write(byte[] data, int offset, int count)
        {
            Body.Write(data, offset, count);
        }

        public Task WriteAsync(string text)
        {
            var writer = new StreamWriter(Body);
            return writer.WriteAsync(text);
        }

        public Task WriteAsync(string text, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}