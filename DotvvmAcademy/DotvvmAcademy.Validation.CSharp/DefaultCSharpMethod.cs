using DotvvmAcademy.Validation.CSharp.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.Validation.CSharp
{
    public class DefaultCSharpMethod : ICSharpMethod
    {
        public void Abstract()
        {
            throw new NotImplementedException();
        }

        public void AccessModifier(CSharpAccessModifier modifier)
        {
            throw new NotImplementedException();
        }

        public void Async()
        {
            throw new NotImplementedException();
        }

        public ICSharpGenericParameter GenericParameter(string name)
        {
            throw new NotImplementedException();
        }

        public void Override()
        {
            throw new NotImplementedException();
        }

        public void Parameters(IEnumerable<CSharpTypeDescriptor> parameters)
        {
            throw new NotImplementedException();
        }

        public void ReturnType(CSharpTypeDescriptor type)
        {
            throw new NotImplementedException();
        }

        public void Static()
        {
            throw new NotImplementedException();
        }

        public void Virtual()
        {
            throw new NotImplementedException();
        }
    }
}
