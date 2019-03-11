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
            var summary = BenchmarkRunner.Run<StepValidation>(DefaultConfig.Instance.With(new EtwProfiler()));
        }
    }
}
