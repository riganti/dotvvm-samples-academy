using DotvvmAcademy.Meta.Syntax;

namespace DotvvmAcademy.Meta
{
    public interface INameBuilder<TMeta>
    {
        NameNode Build(TMeta meta);
    }
}