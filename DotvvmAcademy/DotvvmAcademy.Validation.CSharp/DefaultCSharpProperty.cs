using DotvvmAcademy.Validation.CSharp.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.Validation.CSharp
{
    public class DefaultCSharpProperty : ICSharpProperty
    {
        public void Abstract()
        {
            throw new NotImplementedException();
        }

        public void AccessModifier(CSharpAccessModifier modifier)
        {
            throw new NotImplementedException();
        }

        public ICSharpAccessor Getter()
        {
            throw new NotImplementedException();
        }

        public void Override()
        {
            throw new NotImplementedException();
        }

        public ICSharpAccessor Setter()
        {
            throw new NotImplementedException();
        }

        public void Static()
        {
            throw new NotImplementedException();
        }

        public void Type(CSharpTypeDescriptor type)
        {
            throw new NotImplementedException();
        }

        public void Virtual()
        {
            throw new NotImplementedException();
        }
    }
}
