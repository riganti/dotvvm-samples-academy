using DotvvmAcademy.Validation.CSharp.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.Validation.CSharp
{
    public class DefaultCSharpClass : ICSharpClass
    {
        public void Abstract()
        {
            throw new NotImplementedException();
        }

        public void AccessModifier(CSharpAccessModifier modifier)
        {
            throw new NotImplementedException();
        }

        public ICSharpConstructor Constructor(IEnumerable<CSharpTypeDescriptor> parameters)
        {
            throw new NotImplementedException();
        }

        public void ConversionOperator(CSharpTypeDescriptor parameterType, CSharpTypeDescriptor returnType)
        {
            throw new NotImplementedException();
        }

        public ICSharpDelegate Delegate(string name)
        {
            throw new NotImplementedException();
        }

        public void Destructor()
        {
            throw new NotImplementedException();
        }

        public ICSharpEnum Enum(string name)
        {
            throw new NotImplementedException();
        }

        public ICSharpEvent Event(string name)
        {
            throw new NotImplementedException();
        }

        public ICSharpField Field(string name)
        {
            throw new NotImplementedException();
        }

        public ICSharpGenericParameter GenericParameter(string name)
        {
            throw new NotImplementedException();
        }

        public ICSharpIndexer Indexer(IEnumerable<CSharpTypeDescriptor> parameters)
        {
            throw new NotImplementedException();
        }

        public void InheritsFrom(IEnumerable<CSharpTypeDescriptor> baseTypes)
        {
            throw new NotImplementedException();
        }

        public ICSharpInterface Interface(string name)
        {
            throw new NotImplementedException();
        }

        public ICSharpMethod Method(string name)
        {
            throw new NotImplementedException();
        }

        public ICSharpMethod Operator(string operationName)
        {
            throw new NotImplementedException();
        }

        public void Override()
        {
            throw new NotImplementedException();
        }

        public ICSharpProperty Property(string name)
        {
            throw new NotImplementedException();
        }

        public void Sealed()
        {
            throw new NotImplementedException();
        }

        public void Static()
        {
            throw new NotImplementedException();
        }

        public ICSharpStruct Struct(string name)
        {
            throw new NotImplementedException();
        }
    }
}
