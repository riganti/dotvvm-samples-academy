using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Xml;

namespace DotvvmAcademy.Validation.Dothtml
{
    internal class XPathDothtmlNamespaceResolver : IXmlNamespaceResolver
    {
        private readonly ValidationControlResolver controlResolver;
        private readonly NameTable nameTable;
        private readonly ImmutableDictionary<string, string> namespaces;

        public XPathDothtmlNamespaceResolver(ValidationControlResolver controlResolver, NameTable nameTable)
        {
            this.controlResolver = controlResolver;
            this.nameTable = nameTable;
            namespaces = controlResolver
                .GetRegisteredNamespaces()
                .ToImmutableDictionary(
                    keySelector: p => nameTable.GetOrAdd(p.Key),
                    elementSelector: p => nameTable.GetOrAdd(p.Value.ToDisplayString()));
        }

        public IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
        {
            return namespaces;
        }

        public string LookupNamespace(string prefix)
        {
            if (namespaces.TryGetValue(prefix, out var @namespace))
            {
                return @namespace;
            }

            return null;
        }

        public string LookupPrefix(string namespaceName)
        {
            return namespaces.FirstOrDefault(p => p.Value == namespaceName).Key;
        }
    }
}