using DotvvmAcademy.Validation;
using Microsoft.Extensions.Caching.Memory;
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
    public class CourseWorkspace : IDisposable
    {
        public const int ValidationTimeout = 1000;
#if DEBUG
        public static readonly TimeSpan SourceExpiration = TimeSpan.FromSeconds(3);
#else
        public static readonly TimeSpan SourceExpiration = TimeSpan.FromDays(1);
#endif
        private readonly IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());
        private readonly ICourseEnvironment environment;
        private readonly ImmutableDictionary<Type, object> sourceProviders;

        public CourseWorkspace(ICourseEnvironment environment)
        {
            this.environment = environment;
            sourceProviders = ImmutableDictionary.CreateRange(new Dictionary<Type, object>
            {
                [typeof(Course)] = new CourseProvider(environment),
                [typeof(Lesson)] = new LessonProvider(environment),
                [typeof(LessonVariant)] = new LessonVariantProvider(environment),
                [typeof(Step)] = new StepProvider(environment),
                [typeof(CodeTask)] = new CodeTaskProvider(environment),
                [typeof(CourseFile)] = new CourseFileProvider(environment),
                [typeof(Archive)] = new ArchiveProvider(environment),
            });
        }

        public void Dispose()
        {
            cache.Dispose();
        }

        public async Task<TSource> Load<TSource>(string sourcePath)
            where TSource : Source
        {
            if (!SourcePath.IsAbsolute(sourcePath))
            {
                throw new ArgumentException("Source path must be absolute.", nameof(sourcePath));
            }

            var cacheKey = $"{typeof(TSource)}:{sourcePath}";
            TSource source;
            if (cache.TryGetValue(cacheKey, out source))
            {
                return source;
            }

            var sourceProvider = (ISourceProvider<TSource>)sourceProviders[typeof(TSource)];
            source = await sourceProvider.Get(sourcePath);
            using (var entry = cache.CreateEntry(cacheKey))
            {
                entry.Value = source;
                entry.SetAbsoluteExpiration(SourceExpiration);
            }
            return source;
        }

        public Task<string> Read(string path)
        {
            return environment.Read(path);
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
    }
}