using DotvvmAcademy.Validation.CSharp.Unit;
using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public class DefaultCSharpNamespace : DefaultCSharpObject, ICSharpNamespace
    {
        private readonly ICSharpObjectFactory factory;
        private readonly ICSharpNameFormatter formatter;

        public DefaultCSharpNamespace(ICSharpNameStack nameStack, ICSharpObjectFactory factory, ICSharpNameFormatter formatter) : base(nameStack)
        {
            this.factory = factory;
            this.formatter = formatter;
        }

        public ICSharpClass GetClass(string name, IEnumerable<CSharpGenericParameterDescriptor> genericParameters)
        {
            name = formatter.AppendMember(FullName, name);
            name = formatter.GetGenericName(name, genericParameters);
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
            name = formatter.AppendMember(FullName, name);
            return factory.GetObject<ICSharpNamespace>(name);
        }

        public ICSharpStruct GetStruct(string name, IEnumerable<CSharpGenericParameterDescriptor> genericParameters)
        {
            throw new NotImplementedException();
        }
    }
}