using DotvvmAcademy.Validation.CSharp.Abstractions;
using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp
{
    public class DefaultCSharpClass : ICSharpClass
    {
        private readonly ICSharpFactory factory;

        public DefaultCSharpClass(ICSharpFactory factory)
        {
            this.factory = factory;
        }

        public CSharpAccessModifier AccessModifier { get; set; }

        public IList<CSharpTypeDescriptor> BaseTypes { get; set; } = new List<CSharpTypeDescriptor>();

        public bool HasDestructor { get; set; }

        public bool IsAbstract { get; set; }

        public bool IsOverriding { get; set; }

        public bool IsSealed { get; set; }

        public bool IsStatic { get; set; }

        public ICSharpConstructor GetConstructor(IEnumerable<CSharpTypeDescriptor> parameters)
        {
            throw new NotImplementedException();
        }

        public ICSharpConversionOperator GetConversionOperator(CSharpTypeDescriptor parameterType, CSharpTypeDescriptor returnType)
        {
            throw new NotImplementedException();
        }

        public ICSharpDelegate GetDelegate(string name, IEnumerable<CSharpGenericParameterDescriptor> genericParameters)
        {
            throw new NotImplementedException();
        }

        public ICSharpEnum GetEnum(string name)
        {
            throw new NotImplementedException();
        }

        public ICSharpEvent GetEvent(string name)
        {
            throw new NotImplementedException();
        }

        public ICSharpField GetField(string name)
        {
            throw new NotImplementedException();
        }

        public ICSharpIndexer GetIndexer(IEnumerable<CSharpTypeDescriptor> parameters)
        {
            throw new NotImplementedException();
        }

        public ICSharpInterface GetInterface(string name, IEnumerable<CSharpGenericParameterDescriptor> genericParameters)
        {
            throw new NotImplementedException();
        }

        public ICSharpMethod GetMethod(string name, IEnumerable<CSharpTypeDescriptor> parameters, IEnumerable<CSharpGenericParameterDescriptor> genericParameters)
        {
            throw new NotImplementedException();
        }

        public ICSharpProperty GetProperty(string name)
        {
            throw new NotImplementedException();
        }

        public ICSharpMethod Operator(string operationName)
        {
            throw new NotImplementedException();
        }

        public ICSharpStruct Struct(string name, IEnumerable<CSharpGenericParameterDescriptor> genericParameters)
        {
            throw new NotImplementedException();
        }
    }
}