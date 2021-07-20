using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Xml;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Configuration;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class XPathDothtmlNamespaceResolver : IXmlNamespaceResolver
    {
        private readonly ImmutableDictionary<string, string> namespaces;

        public XPathDothtmlNamespaceResolver(DotvvmMarkupConfiguration markup, NameTable nameTable)
        {
            namespaces = markup.Controls
                .ToImmutableDictionary(
                    keySelector: p => nameTable.GetOrAdd(p.TagPrefix),
                    elementSelector: p => nameTable.GetOrAdd(p.Namespace));
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
