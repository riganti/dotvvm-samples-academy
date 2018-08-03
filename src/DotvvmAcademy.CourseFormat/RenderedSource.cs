namespace DotvvmAcademy.CourseFormat
{
    public abstract class RenderedSource<TSource>
        where TSource : Source
    {
        public RenderedSource(TSource source)
        {
            Source = source;
        }

        public TSource Source { get; }

        public abstract long GetSize();
    }
}