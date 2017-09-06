using DotvvmAcademy.CommonMark.Segments;

namespace DotvvmAcademy.CommonMark
{
    public interface IPlaceholderParsingContext
    {
        string Placeholder { get; }

        void AddSegment(ISegment segment);
    }
}