using Microsoft.CodeAnalysis;
using System;
using System.Reflection;

namespace DotvvmAcademy.Meta
{
    public static class MetadataReferencer
    {
        public static PortableExecutableReference FromAssembly(Assembly assembly)
            => MetadataReference.CreateFromFile(assembly.Location);

        public static PortableExecutableReference FromName(string name)
            => FromAssembly(Assembly.Load(name));

        public static PortableExecutableReference FromType(Type type)
            => FromAssembly(type.Assembly);
    }
}