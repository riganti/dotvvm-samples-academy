using DotvvmAcademy.Validation.CSharp.Unit;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public class DefaultCSharpProperty : DefaultCSharpObject, ICSharpProperty
    {
        private readonly ICSharpObjectFactory factory;
        private readonly ICSharpNameFormatter formatter;

        public DefaultCSharpProperty(ICSharpNameStack nameStack, ICSharpObjectFactory factory, ICSharpNameFormatter formatter) : base(nameStack)
        {
            this.factory = factory;
            this.formatter = formatter;
        }

        public CSharpAccessModifier AccessModifier { get; set; }

        public bool IsAbstract { get; set; }

        public bool IsOverriding { get; set; }

        public bool IsStatic { get; set; }

        public bool IsVirtual { get; set; }

        public CSharpTypeDescriptor Type { get; set; }

        public ICSharpAccessor GetGetter()
        {
            var name = formatter.AppendMember(FullName, CSharpConstants.GetterName);
            return factory.GetObject<ICSharpAccessor>(name);
        }

        public ICSharpAccessor GetSetter()
        {
            var name = formatter.AppendMember(FullName, CSharpConstants.SetterName);
            return factory.GetObject<ICSharpAccessor>(name);
        }
    }
}