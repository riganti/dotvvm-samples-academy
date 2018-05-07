using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public class CSharpObject : ICSharpProject, ICSharpProperty, ICSharpField, ICSharpEvent, ICSharpMethod
    {


        public CSharpAccessibility AccessModifier { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsStatic { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ICSharpEvent GetEvent(string name)
        {
            throw new NotImplementedException();
        }

        public ICSharpField GetField(string name)
        {
            throw new NotImplementedException();
        }

        public ICSharpMethod GetMethod(string name)
        {
            throw new NotImplementedException();
        }

        public ICSharpProject GetProperty(string name)
        {
            throw new NotImplementedException();
        }

        public ICSharpType GetType(string name)
        {
            throw new NotImplementedException();
        }
    }
}
