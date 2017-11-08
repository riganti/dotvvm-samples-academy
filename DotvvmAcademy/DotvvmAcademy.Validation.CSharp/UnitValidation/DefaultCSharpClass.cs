using DotvvmAcademy.Validation.CSharp.UnitValidation.Abstractions;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp.UnitValidation
{
    public class DefaultCSharpClass : DefaultCSharpObject, ICSharpClass
    {
        private readonly ICSharpFactory factory;
        private readonly ICSharpFullNameProvider nameProvider;

        public DefaultCSharpClass(string fullName, ICSharpFactory factory, ICSharpFullNameProvider nameProvider) : base(fullName)
        {
            this.factory = factory;
            this.nameProvider = nameProvider;
        }

        public CSharpAccessModifier AccessModifier { get; set; }

        public IList<CSharpTypeDescriptor> BaseTypes { get; set; } = new List<CSharpTypeDescriptor>();

        public bool HasDestructor { get; set; }

        public bool IsAbstract { get; set; }

        public bool IsSealed { get; set; }

        public bool IsStatic { get; set; }

        public ICSharpClass GetClass(string name, IEnumerable<CSharpGenericParameterDescriptor> genericParameters)
        {
            name = nameProvider.GetMemberName(FullName, name);
            if (genericParameters != null)
            {
                name = nameProvider.GetGenericName(name, genericParameters?.Select(p => p.Name));
            }
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
            name = nameProvider.GetMemberName(FullName, name);
            if (genericParameters != null)
            {
                name = nameProvider.GetGenericName(name, genericParameters.Select(p => p.Name));
            }
            name = nameProvider.GetInvokableName(name, parameters?.Select(p => p.FullName));
            return factory.GetObject<ICSharpMethod>(name);
        }

        public ICSharpMethod GetOperator(string operationName)
        {
            throw new NotImplementedException();
        }

        public ICSharpProperty GetProperty(string name)
        {
            name = nameProvider.GetMemberName(FullName, name);
            return factory.GetObject<ICSharpProperty>(name);
        }

        public ImmutableArray<SyntaxKind> GetRepresentingKind()
        {
            throw new NotImplementedException();
        }

        public ICSharpStruct GetStruct(string name, IEnumerable<CSharpGenericParameterDescriptor> genericParameters)
        {
            throw new NotImplementedException();
        }
    }
}