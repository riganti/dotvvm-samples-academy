using DotvvmAcademy.Validation.CSharp.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp
{
    public class DefaultCSharpNamespace : DefaultCSharpObject, ICSharpNamespace
    {
        private readonly ICSharpFactory factory;
        private readonly ICSharpFullNameProvider nameProvider;

        public DefaultCSharpNamespace(ICSharpFactory factory, ICSharpFullNameProvider nameProvider)
        {
            this.factory = factory;
            this.nameProvider = nameProvider;
        }

        public ICSharpClass GetClass(string name, IEnumerable<CSharpGenericParameterDescriptor> genericParameters)
        {
            name = nameProvider.GetMemberName(FullName, name);
            if (genericParameters != null)
            {
                name = nameProvider.GetGenericName(name, genericParameters.Select(p => p.Name));
            }
            return factory.GetObject<ICSharpClass>(name);
        }

        public ICSharpDelegate GetDelegate(string name)
        {
            throw new NotImplementedException();
        }

        public ICSharpEnum GetEnum(string name)
        {
            throw new NotImplementedException();
        }

        public ICSharpInterface GetInterface(string name, IEnumerable<CSharpGenericParameterDescriptor> genericParameters)
        {
            throw new NotImplementedException();
        }

        public ICSharpNamespace GetNamespace(string name)
        {
            name = nameProvider.GetMemberName(FullName, name);
            return factory.GetObject<ICSharpNamespace>(name);
        }

        public ICSharpStruct GetStruct(string name, IEnumerable<CSharpGenericParameterDescriptor> genericParameters)
        {
            throw new NotImplementedException();
        }
    }
}