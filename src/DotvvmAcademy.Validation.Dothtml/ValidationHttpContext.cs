using DotVVM.Framework.Hosting;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace DotvvmAcademy.Validation.Dothtml
{
    internal class ValidationHttpContext : IHttpContext
    {
        public IHttpRequest Request { get; set; }

        public IHttpResponse Response { get; set; }

        public ClaimsPrincipal User { get; }

        public IEnumerable<Tuple<string, IEnumerable<KeyValuePair<string, object>>>> GetEnvironmentTabs()
        {
            throw new NotImplementedException();
        }

        public T GetItem<T>(string key)
        {
            throw new NotImplementedException();
        }

        public void SetItem<T>(string key, T value)
        {
            throw new NotImplementedException();
        }
    }
}