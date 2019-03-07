using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using DotvvmAcademy.CourseFormat;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotvvmAcademy.Benchmarks
{
    public class StepValidation
    {
        private const string CourseDirectory = @"C:\src\academy\src\DotvvmAcademy.Web\Course";

        [Benchmark]
        public async Task ValidateContentSample()
        {
            var workspace = new CourseWorkspace(new FileSystemEnvironment(new DirectoryInfo(CourseDirectory)));
            var step = await workspace.LoadStep("000_content_sample", "en", "10_markdown");
            var diagnostics = await workspace.ValidateStep(step, "");
            if (diagnostics.Count() != 3)
            {
                throw new InvalidOperationException("There were supposed to be 3 errors.");
            }
        }
    }
}
