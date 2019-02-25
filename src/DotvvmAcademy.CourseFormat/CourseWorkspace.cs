using DotvvmAcademy.Validation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public class CourseWorkspace
    {
        public const int ValidationTimeout = 1000;

        private readonly CourseCache cache = new CourseCache();
        private readonly IServiceProvider provider;

        public CourseWorkspace(CourseCache cache, IServiceProvider provider)
        {
            this.cache = cache;
            this.provider = provider;
        }

        public async Task<TSource> Load<TSource>(string sourcePath)
            where TSource : Source
        {
            if (!SourcePath.IsAbsolute(sourcePath))
            {
                throw new ArgumentException("Source path must be absolute.", nameof(sourcePath));
            }
            if (cache.TryGetValue($"{CourseCache.SourcePrefix}{sourcePath}", out var existingSource))
            {
                return (TSource)existingSource;
            }
            var sourceProvider = provider.GetRequiredService<ISourceProvider<TSource>>();
            var newSource = await sourceProvider.Get(sourcePath);
            if (newSource == null)
            {
                return null;
            }

            cache.AddSource(newSource);
            return newSource;
        }

        public async Task<IEnumerable<CodeTaskDiagnostic>> ValidateStep(
            Step step,
            string code)
        {
            var codeTask = await Load<CodeTask>(step.CodeTaskPath);
            var dependencies = await Task.WhenAll(step.CodeTaskDependencies.Select(d => Load<CourseFile>(d)));
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var args = new StringBuilder($"{directory}/sandbox/DotvvmAcademy.CourseFormat.Sandbox.dll");
            args.Append(' ').Append(codeTask.MapName);
            args.Append(' ').Append(codeTask.EntryTypeName);
            args.Append(' ').Append(codeTask.EntryMethodName);
            foreach (var dependency in dependencies)
            {
                args.Append(' ').Append(dependency.MapName);
            }
            var info = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = args.ToString(),
                UseShellExecute = false,
                CreateNoWindow = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true
            };
            var diagnostics = ImmutableArray.CreateBuilder<CodeTaskDiagnostic>();
            var process = Process.Start(info);
            bool killed = false;
            ThreadPool.QueueUserWorkItem((s) =>
            {
                Thread.Sleep(4000);
                if (!process.HasExited)
                {
                    process.Kill();
                    diagnostics.Add(new CodeTaskDiagnostic(Resources.ERR_ValidationTakesTooLong, -1, -1, CodeTaskDiagnosticSeverity.Error));
                    killed = true;
                }
            });
            await process.StandardInput.WriteAsync(code);
            process.StandardInput.Close();
            try
            {
                var formatter = new BinaryFormatter();
                var deserializedDiagnostics = (LightDiagnostic[])formatter.Deserialize(process.StandardOutput.BaseStream);
                foreach (var deserializedDiagnostic in deserializedDiagnostics)
                {
                    if (deserializedDiagnostic.Source.StartsWith("UserCode"))
                    {
                        diagnostics.Add(new CodeTaskDiagnostic(
                            message: deserializedDiagnostic.Message,
                            start: deserializedDiagnostic.Start,
                            end: deserializedDiagnostic.End,
                            severity: deserializedDiagnostic.Severity.ToCodeTaskDiagnosticSeverity()));
                    }
                }

            }
            catch (SerializationException)
            {
                if (!killed)
                {
                    diagnostics.Add(new CodeTaskDiagnostic("Your code couldn't be validated.", -1, -1, CodeTaskDiagnosticSeverity.Error));
                }
            }
            return diagnostics.ToImmutable();
        }

        public Task<string> Read(string path)
        {
            return provider.GetRequiredService<ICourseEnvironment>().Read(path);
        }
    }
}