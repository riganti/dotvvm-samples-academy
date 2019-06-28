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
            var workspace = new CourseWorkspace();
            await workspace.LoadCourse(CourseDirectory);
            var codeTask = workspace.CurrentCourse.GetLesson("000_content_sample")
                .GetVariant("en")
                .GetStep("markdown")
                .CodeTask;
            var diagnostics = await workspace.ValidateCodeTask(codeTask, "");
            if (diagnostics.Count() != 3)
            {
                throw new InvalidOperationException("There were supposed to be 3 errors.");
            }
        }
    }
}
