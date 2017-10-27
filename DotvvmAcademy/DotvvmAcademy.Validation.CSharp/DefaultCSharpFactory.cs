using DotvvmAcademy.Validation.CSharp.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DotvvmAcademy.Validation.CSharp
{
    public class DefaultCSharpFactory : ICSharpFactory
    {
        private readonly IServiceProvider provider;

        public DefaultCSharpFactory(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public ICSharpClass CreateClass()
        {
            throw new NotImplementedException();
        }

        public ICSharpConstructor CreateConstructor()
        {
            throw new NotImplementedException();
        }

        public ICSharpDelegate CreateDelegate()
        {
            throw new NotImplementedException();
        }

        public ICSharpDocument CreateDocument()
        {
            return provider.GetRequiredService<ICSharpDocument>();
        }

        public ICSharpEnum CreateEnum()
        {
            throw new NotImplementedException();
        }

        public ICSharpEvent CreateEvent()
        {
            throw new NotImplementedException();
        }

        public ICSharpField CreateField()
        {
            throw new NotImplementedException();
        }

        public ICSharpIndexer CreateIndexer()
        {
            throw new NotImplementedException();
        }

        public ICSharpInterface CreateInterface()
        {
            throw new NotImplementedException();
        }

        public ICSharpMethod CreateMethod()
        {
            throw new NotImplementedException();
        }

        public ICSharpNamespace CreateNamespace(string name = "")
        {
            throw new NotImplementedException();
        }

        public ICSharpProperty CreateProperty()
        {
            throw new NotImplementedException();
        }

        public ICSharpStruct CreateStruct()
        {
            throw new NotImplementedException();
        }

        public CSharpValidationMethod CreateValidationMethod()
        {
            throw new NotImplementedException();
        }
    }
}