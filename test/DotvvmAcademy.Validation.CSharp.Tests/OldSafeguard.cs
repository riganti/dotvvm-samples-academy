using System;
using System.Diagnostics;

namespace DotvvmAcademy.Validation.CSharp
{
    public class OldSafeguard
    {
        private readonly Lazy<Stopwatch> stopwatch = new Lazy<Stopwatch>(() => Stopwatch.StartNew());

        public OldSafeguard(int timeLimit = 1)
        {
            TimeLimit = timeLimit;
        }

        public int TimeLimit { get; }

        public void OnInstruction()
        {
            CheckTime();
        }

        private void CheckTime()
        {
            if (stopwatch.Value.ElapsedTicks >= TimeSpan.TicksPerSecond * TimeLimit)
            {
                throw new AssemblySafeguardException($"Your code ran for too long.");
            }
        }
    }
}
