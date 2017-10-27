using DotvvmAcademy.Validation.CSharp.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.Validation.CSharp
{
    public class DefaultCSharpDocument : ICSharpDocument
    {
        public ICSharpNamespace GlobalNamespace()
        {
            throw new NotImplementedException();
        }

        public ICSharpNamespace Namespace(string name)
        {
            throw new NotImplementedException();
        }
    }
}
