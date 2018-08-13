using DotvvmAcademy.Meta.Syntax;
using DotvvmAcademy.Validation.Unit;

namespace DotvvmAcademy.Validation.CSharp.Unit
{
    public class CSharpQuery<TResult> : Query<TResult>
    {
        public CSharpQuery(CSharpUnit unit, NameNode name) : base(unit, name.ToString())
        {
            Unit = unit;
            Name = name;
        }

        public NameNode Name { get; }

        public new CSharpUnit Unit { get; }
    }
}