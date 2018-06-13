using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public class CSharpObject : ICSharpProject, ICSharpProperty, ICSharpField, ICSharpEvent, ICSharpMethod, ICSharpType, IEquatable<CSharpObject>
    {
        private readonly Dictionary<string, MetadataName> nameCache;
        private readonly MetadataNameParser parser;
        private readonly Dictionary<MetadataName, CSharpObject> spawns = new Dictionary<MetadataName, CSharpObject>();

        public CSharpObject(MetadataNameParser parser)
        {
            nameCache = new Dictionary<string, MetadataName>();
            this.parser = parser;
        }

        private CSharpObject(MetadataName name, Dictionary<string, MetadataName> nameCache, MetadataNameParser parser)
        {
            Name = name;
            this.nameCache = nameCache;
            this.parser = parser;
        }

        public CSharpAccessibility Accessibility { get; set; } = CSharpAccessibility.Public;

        public List<DynamicValidationAction> Actions { get; } = new List<DynamicValidationAction>();

        public ICSharpType BaseType { get; set; }

        public ISet<ICSharpType> Interfaces { get; } = new HashSet<ICSharpType>();

        public bool IsAllowed { get; set; } = true;

        public bool IsDeclared { get; set; } = true;

        public bool IsStatic { get; set; } = false;

        public MetadataName Name { get; }

        CSharpTypeKind ICSharpType.TypeKind { get { return TypeKind.Value; } set { TypeKind = value; } }

        public CSharpTypeKind? TypeKind { get; set; }

        public static bool operator !=(CSharpObject one, CSharpObject two)
        {
            return !(one == two);
        }

        public static bool operator ==(CSharpObject one, CSharpObject two)
        {
            return EqualityComparer<CSharpObject>.Default.Equals(one, two);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as CSharpObject);
        }

        public bool Equals(CSharpObject other)
        {
            return other != null && EqualityComparer<MetadataName>.Default.Equals(Name, other.Name);
        }

        public ICSharpEvent GetEvent(string name) => Spawn(name);

        public ICSharpField GetField(string name) => Spawn(name);

        public override int GetHashCode()
        {
            return 363513814 + EqualityComparer<MetadataName>.Default.GetHashCode(Name);
        }

        public MetadataCollection<MetadataName> GetMetadata()
        {
            var metadata = new MetadataCollection<MetadataName>();
            foreach (var pair in spawns)
            {
                pair.Value.PopulateMetadata(metadata);
            }
            return metadata;
        }

        public ICSharpMethod GetMethod(string name) => Spawn(name);

        public ICSharpProject GetProperty(string name) => Spawn(name);

        public ICSharpType GetType(string name) => Spawn(name);

        public void Validate(DynamicValidationAction action)
        {
            Actions.Add(action);
        }

        private MetadataName GetName(string name)
        {
            if (!nameCache.TryGetValue(name, out var metadataName))
            {
                metadataName = parser.Parse(name);
                nameCache[name] = metadataName;
            }
            return metadataName;
        }

        private void PopulateMetadata(MetadataCollection<MetadataName> metadata)
        {
            metadata[Name, DeclarationExistenceAnalyzer.MetadataKey] = IsDeclared;
            metadata[Name, SymbolAllowedAnalyzer.MetadataKey] = IsAllowed;
            metadata[Name, SymbolAccessibilityAnalyzer.MetadataKey] = Accessibility;
            if (TypeKind != null)
            {
                metadata[Name, TypeKindAnalyzer.MetadataKey] = TypeKind.Value;
            }
            if (BaseType != null)
            {
                metadata[Name, BaseTypeAnalyzer.MetadataKey] = ((CSharpObject)BaseType).Name;
            }
            metadata[Name, SymbolStaticAnalyzer.MetadataKey] = IsStatic;
            if (Interfaces.Count > 0)
            {
                metadata[Name, InterfaceImplementationAnalyzer.PositiveMetadataKey] = Interfaces.ToImmutableArray();
            }
            foreach (var pair in spawns)
            {
                pair.Value.PopulateMetadata(metadata);
            }
        }

        private CSharpObject Spawn(MetadataName name)
        {
            if (!spawns.TryGetValue(name, out var spawn))
            {
                spawn = new CSharpObject(name, nameCache, parser);
                spawns[name] = spawn;
            }
            return spawn;
        }

        private CSharpObject Spawn(string name) => Spawn(GetName(name));
    }
}