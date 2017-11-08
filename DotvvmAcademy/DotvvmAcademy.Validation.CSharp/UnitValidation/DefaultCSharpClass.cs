using DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions;
using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.UnitValidation
{
    public class DefaultCSharpClass : DefaultCSharpObject, ICSharpClass
    {
        private readonly ICSharpFactory factory;
        private readonly ICSharpNameFormatter formatter;

        public DefaultCSharpClass(ICSharpNameStack nameStack, ICSharpFactory factory, ICSharpNameFormatter formatter) : base(nameStack)
        {
            this.factory = factory;
            this.formatter = formatter;
        }

        public CSharpAccessModifier AccessModifier { get; set; }

        public IList<CSharpTypeDescriptor> BaseTypes { get; set; } = new List<CSharpTypeDescriptor>();

        public bool HasDestructor { get; set; }

        public bool IsAbstract { get; set; }

        public bool IsSealed { get; set; }

        public bool IsStatic { get; set; }

        public ICSharpClass GetClass(string name, IEnumerable<CSharpGenericParameterDescriptor> genericParameters)
        {
            name = formatter.GetComplexName(FullName, name, genericParameters);
            return factory.GetObject<ICSharpClass>(name);
        }

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

        public CSharpTypeDescriptor GetDescriptor()
        {
            return new CSharpTypeDescriptor(FullName);
        }

        public ICSharpEnum GetEnum(string name)
        {
            name = formatter.AppendMember(FullName, name);
            return factory.GetObject<ICSharpEnum>(name);
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

        public ICSharpMethod GetMethod(string name, IEnumerable<CSharpGenericParameterDescriptor> genericParameters, IEnumerable<CSharpTypeDescriptor> parameters)
        {
            name = formatter.GetComplexInvokableName(FullName, name, genericParameters, parameters);
            return factory.GetObject<ICSharpMethod>(name);
        }

        public ICSharpMethod GetOperator(string operationName)
        {
            throw new NotImplementedException();
        }

        public ICSharpProperty GetProperty(string name)
        {
            name = formatter.AppendMember(FullName, name);
            return factory.GetObject<ICSharpProperty>(name);
        }

        public ICSharpStruct GetStruct(string name, IEnumerable<CSharpGenericParameterDescriptor> genericParameters)
        {
            throw new NotImplementedException();
        }
    }
}