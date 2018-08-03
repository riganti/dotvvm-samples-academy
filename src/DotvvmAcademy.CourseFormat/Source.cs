using Microsoft.Extensions.Primitives;
using System.Threading;

namespace DotvvmAcademy.CourseFormat
{
    public abstract class Source
    {
        CancellationTokenSource tokenSource = new CancellationTokenSource();

        public Source(string path)
        {
            Path = path;
            EvictionToken = new CancellationChangeToken(tokenSource.Token);
        }

        internal IChangeToken EvictionToken { get; }

        public string Path { get; }

        public abstract long GetSize();

        internal void OnEviction()
        {
            tokenSource.Cancel();
        }
    }
}