using DotvvmAcademy.Validation.CSharp.Abstractions;

namespace DotvvmAcademy.Validation.CSharp.UnitValidation
{
    public class DefaultCSharpProperty : DefaultCSharpObject, ICSharpProperty
    {
        private readonly ICSharpFactory factory;
        private readonly ICSharpFullNameProvider nameProvider;

        public DefaultCSharpProperty(ICSharpFactory factory, ICSharpFullNameProvider nameProvider)
        {
            this.factory = factory;
            this.nameProvider = nameProvider;
        }

        public CSharpAccessModifier AccessModifier { get; set; }

        public bool IsAbstract { get; set; }

        public bool IsOverriding { get; set; }

        public bool IsStatic { get; set; }

        public bool IsVirtual { get; set; }

        public CSharpTypeDescriptor Type { get; set; }

        public ICSharpAccessor GetGetter()
        {
            var name = nameProvider.GetMemberName(FullName, CSharpConstants.GetterName);
            name = nameProvider.GetInvokableName(name, null);
            return factory.GetObject<ICSharpAccessor>(name);
        }

        public ICSharpAccessor GetSetter()
        {
            var name = nameProvider.GetMemberName(FullName, CSharpConstants.SetterName);
            name = nameProvider.GetInvokableName(name, null);
            return factory.GetObject<ICSharpAccessor>(name);
        }
    }
}