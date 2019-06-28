using System;
using System.Diagnostics;

namespace DotvvmAcademy.Validation.CSharp
{
    public class AssemblySafeguard
    {
        private const long Limit = 1000;
        private Lazy<Stopwatch> stopwatch = new Lazy<Stopwatch>(() => Stopwatch.StartNew());

        public void Check()
        {
            if (stopwatch.Value.ElapsedMilliseconds >= Limit)
            {
                throw new AssemblySafeguardException($"Your code ran for too long.");
            }
        }
    }
}