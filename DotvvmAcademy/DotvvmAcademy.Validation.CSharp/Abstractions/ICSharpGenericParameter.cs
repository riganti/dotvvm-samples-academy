namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    /// <summary>
    /// A C# generic type parameter.
    /// </summary>
    public interface ICSharpGenericParameter : ICSharpAllowsInheritance
    {
        void ClassConstraint();

        void Contravariant();

        void Covariant();

        void Invariant();

        void NewConstraint();

        void StructConstraint();
    }
}