using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public interface ISourceProvider<TSource>
        where TSource : Source
    {
        Task<TSource> Get(string path);
    }
}