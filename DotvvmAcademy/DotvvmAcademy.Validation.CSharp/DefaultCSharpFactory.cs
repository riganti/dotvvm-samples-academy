using DotvvmAcademy.Validation.CSharp.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;

namespace DotvvmAcademy.Validation.CSharp
{
    public class DefaultCSharpFactory : ICSharpFactory
    {
        private readonly ConcurrentDictionary<string, ICSharpClass> classes = new ConcurrentDictionary<string, ICSharpClass>();
        private readonly ConcurrentDictionary<string, ICSharpConstructor> constructors = new ConcurrentDictionary<string, ICSharpConstructor>();
        private readonly ConcurrentDictionary<string, ICSharpDelegate> delegates = new ConcurrentDictionary<string, ICSharpDelegate>();
        private readonly ConcurrentDictionary<string, ICSharpEnum> enums = new ConcurrentDictionary<string, ICSharpEnum>();
        private readonly ConcurrentDictionary<string, ICSharpEvent> events = new ConcurrentDictionary<string, ICSharpEvent>();
        private readonly ConcurrentDictionary<string, ICSharpField> fields = new ConcurrentDictionary<string, ICSharpField>();
        private readonly ConcurrentDictionary<string, ICSharpIndexer> indexers = new ConcurrentDictionary<string, ICSharpIndexer>();
        private readonly ConcurrentDictionary<string, ICSharpInterface> interfaces = new ConcurrentDictionary<string, ICSharpInterface>();
        private readonly ConcurrentDictionary<string, ICSharpMethod> methods = new ConcurrentDictionary<string, ICSharpMethod>();
        private readonly ConcurrentDictionary<string, ICSharpNamespace> namespaces = new ConcurrentDictionary<string, ICSharpNamespace>();
        private readonly ConcurrentDictionary<string, ICSharpProperty> properties = new ConcurrentDictionary<string, ICSharpProperty>();
        private readonly IServiceProvider provider;
        private readonly ConcurrentDictionary<string, ICSharpStruct> structs = new ConcurrentDictionary<string, ICSharpStruct>();

        public DefaultCSharpFactory(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public ICSharpClass CreateClass(string fullName) => GetOrAdd(classes, fullName);

        public ICSharpConstructor CreateConstructor(string fullName) => GetOrAdd(constructors, fullName);

        public ICSharpDelegate CreateDelegate(string fullName) => GetOrAdd(delegates, fullName);

        public ICSharpDocument CreateDocument() => provider.GetRequiredService<ICSharpDocument>();

        public ICSharpEnum CreateEnum(string fullName) => GetOrAdd(enums, fullName);

        public ICSharpEvent CreateEvent(string fullName) => GetOrAdd(events, fullName);

        public ICSharpField CreateField(string fullName) => GetOrAdd(fields, fullName);

        public ICSharpIndexer CreateIndexer(string fullName) => GetOrAdd(indexers, fullName);

        public ICSharpInterface CreateInterface(string fullName) => GetOrAdd(interfaces, fullName);

        public ICSharpMethod CreateMethod(string fullName) => GetOrAdd(methods, fullName);

        public ICSharpNamespace CreateNamespace(string fullName) => GetOrAdd(namespaces, fullName);

        public ICSharpProperty CreateProperty(string fullName) => GetOrAdd(properties, fullName);

        public ICSharpStruct CreateStruct(string fullName) => GetOrAdd(structs, fullName);

        public CSharpValidationMethod CreateValidationMethod()
        {
            throw new NotImplementedException();
        }

        private TCSharp GetOrAdd<TCSharp>(ConcurrentDictionary<string, TCSharp> dict, string fullName)
        {
            return dict.GetOrAdd(fullName, _ => provider.GetRequiredService<TCSharp>());
        }
    }
}