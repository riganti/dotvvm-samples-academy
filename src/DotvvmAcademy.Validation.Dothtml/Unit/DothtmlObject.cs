using DotVVM.Framework.Binding;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    public class DothtmlObject : IDothtmlDocument, IDothtmlControl, IEquatable<DothtmlObject>
    {
        private readonly Dictionary<DotvvmProperty, BindingMetadata> bindings = new Dictionary<DotvvmProperty, BindingMetadata>();
        private readonly DothtmlIdentifier identifier;
        private readonly Dictionary<string, DothtmlIdentifier> identifierCache;
        private readonly DothtmlIdentifierParser parser;
        private readonly Dictionary<DothtmlIdentifier, DothtmlObject> spawns = new Dictionary<DothtmlIdentifier, DothtmlObject>();
        private readonly Dictionary<DotvvmProperty, PropertyValueMetadata> values = new Dictionary<DotvvmProperty, PropertyValueMetadata>();

        public DothtmlObject(DothtmlIdentifierParser parser)
        {
            identifierCache = new Dictionary<string, DothtmlIdentifier>();
            this.parser = parser;
        }

        private DothtmlObject(DothtmlIdentifier identifier, Dictionary<string, DothtmlIdentifier> identifierCache, DothtmlIdentifierParser parser)
        {
            this.parser = parser;
            this.identifier = identifier;
            this.identifierCache = identifierCache;
        }

        public IDothtmlControl this[int index] => GetControl($"{identifier}[{index}]");

        public static bool operator !=(DothtmlObject object1, DothtmlObject object2)
        {
            return !(object1 == object2);
        }

        public static bool operator ==(DothtmlObject object1, DothtmlObject object2)
        {
            return EqualityComparer<DothtmlObject>.Default.Equals(object1, object2);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as DothtmlObject);
        }

        public bool Equals(DothtmlObject other)
        {
            return other != null &&
                   EqualityComparer<DothtmlIdentifier>.Default.Equals(identifier, other.identifier);
        }

        public IDothtmlControl GetControl(string path)
        {
            var identifier = GetIdentifier(path);
            if (!spawns.TryGetValue(identifier, out var spawn))
            {
                spawn = new DothtmlObject(identifier, identifierCache, parser);
            }
            return spawn;
        }

        public override int GetHashCode()
        {
            return 1442482158 + EqualityComparer<DothtmlIdentifier>.Default.GetHashCode(identifier);
        }

        public MetadataCollection<DothtmlIdentifier> GetMetadata()
        {
            var metadata = new MetadataCollection<DothtmlIdentifier>();
            foreach (var pair in spawns)
            {
                pair.Value.PopulateMetadata(metadata);
            }
            return metadata;
        }

        public void ValidateBinding<TBinding>(DotvvmProperty property, IEnumerable<string> acceptedValues)
        {
            if (!bindings.TryGetValue(property, out var bindingMetadata))
            {
                bindingMetadata = new BindingMetadata
                {
                    Property = property
                };
                bindings[property] = bindingMetadata;
            }
            bindingMetadata.BindingType = typeof(TBinding);
            bindingMetadata.AcceptedValues = acceptedValues.ToImmutableArray();
        }

        public void ValidateValue(DotvvmProperty property, IEnumerable<object> acceptedValues)
        {
            if (!values.TryGetValue(property, out var valueMetadata))
            {
                valueMetadata = new PropertyValueMetadata
                {
                    Property = property
                };
            }
            valueMetadata.AcceptedValues = acceptedValues.ToImmutableArray();
        }

        private DothtmlIdentifier GetIdentifier(string path)
        {
            if (!identifierCache.TryGetValue(path, out var identifier))
            {
                identifier = parser.Parse(path);
                identifierCache[path] = identifier;
            }
            return identifier;
        }

        private void PopulateMetadata(MetadataCollection<DothtmlIdentifier> metadata)
        {
            metadata[identifier, NodeExistenceVisitor.MetadataKey] = true;
            metadata[identifier, BindingVisitor.MetadataKey] = bindings.Values.ToImmutableArray();
            metadata[identifier, PropertyValueVisitor.MetadataKey] = values.Values.ToImmutableArray();
            foreach (var spawn in spawns)
            {
                spawn.Value.PopulateMetadata(metadata);
            }
        }
    }
}