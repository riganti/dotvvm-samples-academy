using DotvvmAcademy.Validation;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public class CourseWorkspace : IDisposable
    {
        public const string SandboxPath = "./sandbox/DotvvmAcademy.CourseFormat.Sandbox.dll";
        public const int ValidationTimeout = 10000;
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

            var cacheKey = $"{typeof(TSource).Name}:{sourcePath}";
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
                entry.RegisterPostEvictionCallback((key, value, reason, state) =>
                {
                    if (value is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                });
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
            var validationId = Guid.NewGuid();

            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var args = new List<string>();
            args.Add(Path.Combine(directory, SandboxPath));

            MemoryMappedFile scriptAssemblyMap;
            {
                var codeTask = await Load<CodeTask>(step.CodeTaskPath);
                var mapName = $"CodeTask/{validationId}";
                scriptAssemblyMap = MemoryMappedFile.CreateNew(mapName, codeTask.Assembly.Length, MemoryMappedFileAccess.ReadWrite);
                using (var mapStream = scriptAssemblyMap.CreateViewStream(0, 0, MemoryMappedFileAccess.ReadWrite))
                {
                    await mapStream.WriteAsync(codeTask.Assembly, 0, codeTask.Assembly.Length);
                }
                args.Add(mapName);
                args.Add(codeTask.EntryTypeName);
                args.Add(codeTask.EntryMethodName);
            }

            var dependencyMaps = new List<MemoryMappedFile>();
            var dependencies = await Task.WhenAll(step.CodeTaskDependencies.Select(d => Load<CourseFile>(d)));
            foreach (var dependency in dependencies)
            {
                var mapName = $"CourseFile/{validationId}/{dependency.Path}";
                var map = MemoryMappedFile.CreateNew(mapName, dependency.Text.Length * sizeof(char), MemoryMappedFileAccess.ReadWrite);
                using (var mapStream = map.CreateViewStream(0, 0, MemoryMappedFileAccess.ReadWrite))
                using (var writer = new StreamWriter(mapStream))
                {
                    await writer.WriteAsync(dependency.Text);
                }
                dependencyMaps.Add(map);
                args.Add(mapName);
            }

            var info = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = string.Join(" ", args),
                UseShellExecute = false,
                CreateNoWindow = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true
            };
            var diagnostics = ImmutableArray.CreateBuilder<CodeTaskDiagnostic>();
            bool killed = false;
            try
            {
                var process = Process.Start(info);
#pragma warning disable CS4014
                Task.Run(async () =>
                {
                    await Task.Delay(ValidationTimeout);
                    if (!process.HasExited)
                    {
                        killed = true;
                        diagnostics.Add(new CodeTaskDiagnostic(Resources.ERR_ValidationTakesTooLong, -1, -1, CodeTaskDiagnosticSeverity.Error));
                        process.Kill();
                    }
                });
#pragma warning restore CS4014
                await process.StandardInput.WriteAsync(code);
                process.StandardInput.Close();
                var formatter = new BinaryFormatter();
                var deserializedDiagnostics = (LightDiagnostic[])formatter.Deserialize(process.StandardOutput.BaseStream);
                // TODO: This Distinct call is a hack
                foreach (var deserializedDiagnostic in deserializedDiagnostics.Distinct())
                {
                    if (deserializedDiagnostic.Source == null || deserializedDiagnostic.Source.StartsWith("UserCode"))
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
            finally
            {
                scriptAssemblyMap.Dispose();
                foreach (var dependencyMap in dependencyMaps)
                {
                    dependencyMap.Dispose();
                }
            }
            return diagnostics.ToImmutable();
        }
    }
}