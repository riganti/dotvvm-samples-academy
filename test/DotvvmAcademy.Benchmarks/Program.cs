using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnostics.Windows;
using BenchmarkDotNet.Running;
using System;

namespace DotvvmAcademy.Benchmarks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            BenchmarkRunner.Run<StepValidation>(DefaultConfig.Instance.AddDiagnoser(new EtwProfiler()));
        }
    }
}
