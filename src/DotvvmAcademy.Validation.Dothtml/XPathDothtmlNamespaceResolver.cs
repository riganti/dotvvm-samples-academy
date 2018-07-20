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
        private readonly ImmutableDictionary<string, string> namespaces;

        public XPathDothtmlNamespaceResolver(ValidationControlResolver controlResolver)
        {
            this.controlResolver = controlResolver;
            namespaces = controlResolver
                .GetRegisteredNamespaces()
                .ToImmutableDictionary(p => p.Key, p => p.Value.ToDisplayString());
        }

        public IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
            => namespaces;

        public string LookupNamespace(string prefix)
        {
            if (namespaces.TryGetValue(prefix, out var @namespace))
            {
                return @namespace;
            }

            return null;
        }

        public string LookupPrefix(string namespaceName)
            => namespaces.FirstOrDefault(p => p.Value == namespaceName).Key;
    }
}