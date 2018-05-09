using System.Collections.Generic;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public interface ICSharpType : ICSharpAllowsAccessModifier, ICSharpAllowsStaticModifier, ICSharpAllowsDeclaration, ICSharpAllowable
    {
        CSharpTypeKind TypeKind { get; set; }

        ICSharpType BaseType { get; set; }

        ISet<ICSharpType> Interfaces { get; }

        ICSharpEvent GetEvent(string name);

        ICSharpField GetField(string name);

        ICSharpMethod GetMethod(string name);

        ICSharpProject GetProperty(string name);

        ICSharpType GetType(string name);
    }
}