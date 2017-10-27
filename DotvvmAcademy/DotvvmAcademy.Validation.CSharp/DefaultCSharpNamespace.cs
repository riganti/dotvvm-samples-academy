using DotvvmAcademy.Validation.CSharp.Abstractions;
using System;

namespace DotvvmAcademy.Validation.CSharp
{
    public class DefaultCSharpNamespace : ICSharpNamespace
    {
        private readonly ICSharpFactory factory;
        private readonly ICSharpFullNameProvider nameProvider;

        public DefaultCSharpNamespace(ICSharpFactory factory, ICSharpFullNameProvider nameProvider)
        {
            this.factory = factory;
            this.nameProvider = nameProvider;
        }

        public string FullName { get; set; }

        public ICSharpClass Class(string name)
        {
            throw new NotImplementedException();
        }

        public ICSharpDelegate Delegate(string name)
        {
            throw new NotImplementedException();
        }

        public ICSharpEnum Enum(string name)
        {
            throw new NotImplementedException();
        }

        public ICSharpInterface Interface(string name)
        {
            throw new NotImplementedException();
        }

        public ICSharpNamespace Namespace(string name)
        {
            return factory.CreateNamespace(nameProvider.GetNestedNamespaceFullName(FullName, name));
        }

        public ICSharpStruct Struct(string name)
        {
            throw new NotImplementedException();
        }
    }
}