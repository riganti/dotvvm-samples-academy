using Microsoft.CodeAnalysis;
using System;
using System.Reflection;

namespace DotvvmAcademy.Meta
{
    public static class RoslynReference
    {
        public static PortableExecutableReference FromAssembly(Assembly assembly)
        {
            return MetadataReference.CreateFromFile(assembly.Location);
        }

        public static PortableExecutableReference FromName(string name)
        {
            return FromAssembly(Assembly.Load(name));
        }
    }
}