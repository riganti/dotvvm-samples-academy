using Microsoft.Extensions.Primitives;
using System.Threading;

namespace DotvvmAcademy.CourseFormat
{
    public abstract class Source
    {
        private readonly CancellationTokenSource tokenSource = new CancellationTokenSource();

        public Source(string path)
        {
            Path = path;
            EvictionToken = new CancellationChangeToken(tokenSource.Token);
        }

        public string Path { get; }

        internal IChangeToken EvictionToken { get; }

        public abstract long GetSize();

        internal void OnEviction()
        {
            tokenSource.Cancel();
        }
    }
}