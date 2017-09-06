using System.Threading.Tasks;

namespace DotvvmAcademy.CommonMark.Parsers
{
    public interface IPlaceholderParser
    {
        Task<bool> Parse(IPlaceholderParsingContext context);
    }
}