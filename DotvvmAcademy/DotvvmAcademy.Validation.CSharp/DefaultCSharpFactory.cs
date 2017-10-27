using DotvvmAcademy.Validation.CSharp.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp
{
    public class DefaultCSharpFactory : ICSharpFactory
    {
        private readonly IServiceProvider provider;
        private readonly Dictionary<string, ICSharpClass> classes = new Dictionary<string, ICSharpClass>();
        private readonly Dictionary<string, ICSharpConstructor> constructors = new Dictionary<string, ICSharpConstructor>();
        private readonly Dictionary<string, ICSharpDelegate> delegates = new Dictionary<string, ICSharpDelegate>();
        private readonly Dictionary<string, ICSharpEnum> enums = new Dictionary<string, ICSharpEnum>();
        private readonly Dictionary<string, ICSharpEvent> events = new Dictionary<string, ICSharpEvent>();
        private readonly Dictionary<string, ICSharpField> fields = new Dictionary<string, ICSharpField>();
        private readonly Dictionary<string, ICSharpIndexer> indexers = new Dictionary<string, ICSharpIndexer>();
        private readonly Dictionary<string, ICSharpInterface> interfaces = new Dictionary<string, ICSharpInterface>();
        private readonly Dictionary<string, ICSharpMethod> methods = new Dictionary<string, ICSharpMethod>();
        private readonly Dictionary<string, ICSharpNamespace> namespaces = new Dictionary<string, ICSharpNamespace>();
        private readonly Dictionary<string, ICSharpProperty> properties = new Dictionary<string, ICSharpProperty>();
        private readonly Dictionary<string, ICSharpStruct> structs = new Dictionary<string, ICSharpStruct>();

        public DefaultCSharpFactory(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public ICSharpClass CreateClass(string fullName)
        {
            throw new NotImplementedException();
        }

        public ICSharpConstructor CreateConstructor(string fullName)
        {
            throw new NotImplementedException();
        }

        public ICSharpDelegate CreateDelegate(string fullName)
        {
            throw new NotImplementedException();
        }

        public ICSharpDocument CreateDocument()
        {
            return provider.GetRequiredService<ICSharpDocument>();
        }

        public ICSharpEnum CreateEnum(string fullName)
        {
            throw new NotImplementedException();
        }

        public ICSharpEvent CreateEvent(string fullName)
        {
            throw new NotImplementedException();
        }

        public ICSharpField CreateField(string fullName)
        {
            throw new NotImplementedException();
        }

        public ICSharpIndexer CreateIndexer(string fullName)
        {
            throw new NotImplementedException();
        }

        public ICSharpInterface CreateInterface(string fullName)
        {
            throw new NotImplementedException();
        }

        public ICSharpMethod CreateMethod(string fullName)
        {
            throw new NotImplementedException();
        }

        public ICSharpNamespace CreateNamespace(string fullName)
        {
            throw new NotImplementedException();
        }

        public ICSharpProperty CreateProperty(string fullName)
        {
            throw new NotImplementedException();
        }

        public ICSharpStruct CreateStruct(string fullName)
        {
            throw new NotImplementedException();
        }

        public CSharpValidationMethod CreateValidationMethod()
        {
            throw new NotImplementedException();
        }
    }
}