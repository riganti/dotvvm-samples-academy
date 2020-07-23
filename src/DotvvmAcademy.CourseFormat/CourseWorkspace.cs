﻿using DotvvmAcademy.Meta;
using DotvvmAcademy.Validation;
using Markdig;
using Markdig.Extensions.Yaml;
using Markdig.Renderers;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace DotvvmAcademy.CourseFormat
{
    public class CourseWorkspace : IDisposable
    {
        public const string SandboxPath = "./sandbox/DotvvmAcademy.CourseFormat.Sandbox.dll";
        public const int ValidationTimeout = 10000;
        public const string LessonFile = "lesson.md";
        public const string VariantFile = "variant.md";
        public const string CSharpLanguage = "csharp";
        public const string DothtmlLanguage = "dothtml";
#if DEBUG
        public static readonly TimeSpan CacheEntryExpiration = TimeSpan.FromSeconds(3);
#else
        public static readonly TimeSpan CacheEntryExpiration = TimeSpan.FromDays(1);
#endif
        private readonly IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());
        private readonly MarkdownPipeline markdigPipeline = new MarkdownPipelineBuilder()
            .UsePipeTables()
            .UseEmphasisExtras()
            .UseYamlFrontMatter()
            .UseFigures()
            .Build();
        private readonly IDeserializer yamlDeserializer = new DeserializerBuilder().Build();

        public Course CurrentCourse { get; set; }

        public void Dispose()
        {
            cache.Dispose();
        }

        public async Task LoadCourse(string directoryPath)
        {
            var directory = new DirectoryInfo(directoryPath);
            if (!directory.Exists)
            {
                throw new ArgumentException("Course directory does not exist.", nameof(directoryPath));
            }

            var lessonDirectories = directory.GetDirectories()
                .Where(d => !d.Name.StartsWith("."))
                .OrderBy(d => d.Name);
            var lessonBuilder = ImmutableArray.CreateBuilder<Lesson>();
            foreach (var lessonDirectory in lessonDirectories)
            {
                var lessonFile = lessonDirectory.GetFiles(LessonFile)
                    .SingleOrDefault();
                if (lessonFile == default)
                {
                    throw new InvalidOperationException($"Lesson at \"{lessonDirectory}\" does not contain a lesson file.");
                }
                var lessonFrontMatter = await ParseFrontMatter<LessonFrontMatter>(lessonFile.FullName);
                var lessonMoniker = lessonFrontMatter.Moniker ?? lessonDirectory.Name;
                var variantDirectories = lessonDirectory.GetDirectories()
                    .Where(d => !d.Name.StartsWith("."))
                    .OrderBy(d => d.Name);
                var variantBuilder = ImmutableArray.CreateBuilder<LessonVariant>();
                foreach (var variantDirectory in variantDirectories)
                {
                    var variantFile = variantDirectory.GetFiles(VariantFile)
                        .SingleOrDefault();
                    if (variantFile == default)
                    {
                        throw new InvalidOperationException($"Lesson variant at \"{variantDirectory}\" does not contain a variant file.");
                    }
                    var variantFrontMatter = await ParseFrontMatter<LessonVariantFrontMatter>(variantFile.FullName);
                    var variantMoniker = variantFrontMatter.Moniker ?? variantDirectory.Name;
                    var stepFiles = variantDirectory.GetFiles("*.md")
                        .Where(f => f.Name != VariantFile)
                        .OrderBy(f => f.Name);
                    var stepBuilder = ImmutableArray.CreateBuilder<Step>();
                    foreach (var stepFile in stepFiles)
                    {
                        var stepFrontMatter = await ParseFrontMatter<StepFrontMatter>(stepFile.FullName);
                        var stepMoniker = stepFrontMatter.Moniker ?? stepFile.Name;
                        Archive archive = null;
                        if (stepFrontMatter.Archive != null)
                        {
                            archive = new Archive(
                                GetAbsolutePath(directory.FullName, stepFile.FullName, stepFrontMatter.Archive.Path),
                                stepFrontMatter.Archive.Name ?? lessonMoniker);
                        }
                        CodeTask codeTask = null;
                        if (stepFrontMatter.CodeTask != null)
                        {
                            var match = Regex.Match(stepFrontMatter.CodeTask.Path, @"\w+\.(\w+)\.csx");
                            var codeLanguage = match.Groups.Count == 2 ? match.Groups[1].Value : null;
                            codeTask = new CodeTask(
                                GetAbsolutePath(directory.FullName, stepFile.FullName, stepFrontMatter.CodeTask.Path),
                                GetAbsolutePath(directory.FullName, stepFile.FullName, stepFrontMatter.CodeTask.Correct),
                                GetAbsolutePath(directory.FullName, stepFile.FullName, stepFrontMatter.CodeTask.Default),
                                ResolveDependencies(directory.FullName, stepFile.FullName, stepFrontMatter.CodeTask.Dependencies),
                                codeLanguage);
                        }
                        EmbeddedView embeddedView = null;
                        if (stepFrontMatter.EmbeddedView != null)
                        {
                            embeddedView = new EmbeddedView(
                                GetAbsolutePath(directory.FullName, stepFile.FullName, stepFrontMatter.EmbeddedView.Path),
                                ResolveDependencies(directory.FullName, stepFile.FullName, stepFrontMatter.EmbeddedView.Dependencies));
                        }
                        var step = new Step(
                            stepFile.FullName,
                            stepMoniker,
                            variantMoniker,
                            lessonMoniker,
                            stepFrontMatter.Title,
                            codeTask,
                            archive,
                            embeddedView);
                        stepBuilder.Add(step);
                    }
                    var variant = new LessonVariant(
                        variantDirectory.FullName,
                        variantMoniker,
                        lessonMoniker,
                        variantFile.FullName,
                        variantFrontMatter.Image,
                        variantFrontMatter.Title,
                        variantFrontMatter.Status,
                        stepBuilder);
                    variantBuilder.Add(variant);
                }
                var lesson = new Lesson(
                    lessonDirectory.FullName,
                    lessonMoniker,
                    variantBuilder);
                lessonBuilder.Add(lesson);
            }
            CurrentCourse = new Course(directory.FullName, lessonBuilder);
        }

        public async Task<string> GetLessonVariantAnnotation(LessonVariant variant)
        {
            var key = $"LessonVariantAnnotation|{variant.AnnotationPath}";
            if (!cache.TryGetValue(key, out string annotation))
            {
                annotation = RenderMarkdown(await ReadFile(variant.AnnotationPath));
                Cache(key, annotation);
            }
            return annotation;
        }

        public async Task<string> GetStepText(Step step)
        {
            var key = $"StepText|{step.Path}";
            if (!cache.TryGetValue(key, out string text))
            {
                text = RenderMarkdown(await ReadFile(step.Path));
                Cache(key, text);
            }
            return text;
        }

        public async Task<byte[]> GetArchiveBytes(Archive archive)
        {
            var key = $"ArchiveBytes|{archive.Path}";
            if (cache.TryGetValue(key, out byte[] bytes))
            {
                return bytes;
            }
            async Task AddRecursive(string directory, ZipArchive zip)
            {
                var info = new DirectoryInfo(Path.Combine(archive.Path, directory));
                foreach (var file in info.GetFiles())
                {
                    var entry = zip.CreateEntry(Path.Combine(directory, file.Name));
                    using var inputStream = file.OpenRead();
                    using var outputStream = entry.Open();
                    await inputStream.CopyToAsync(outputStream);
                }
                foreach (var subdirectory in info.GetDirectories())
                {
                    await AddRecursive($"{directory}{subdirectory.Name}/", zip);
                }
            }

            using var memoryStream = new MemoryStream();
            using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                await AddRecursive("", zipArchive);
            }
            bytes = memoryStream.ToArray();
            Cache(key, bytes);
            return bytes;
        }

        public async Task<string> GetFileContents(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new InvalidOperationException("File doesn't exist.");
            }
            var key = $"FileContents|{filePath}";
            if (cache.TryGetValue(key, out string content))
            {
                return content;
            }

            content = await ReadFile(filePath);
            Cache(key, content);
            return content;
        }

        public async Task<ValidationScript> GetValidationScript(string scriptPath)
        {
            var scriptText = await GetFileContents(scriptPath);
            var scriptOptions = ScriptOptions.Default
                .AddReferences(
                    RoslynReference.FromName("netstandard"),
                    RoslynReference.FromName("System.Private.CoreLib"),
                    RoslynReference.FromName("System.Runtime"),
                    RoslynReference.FromName("System.Collections"),
                    RoslynReference.FromName("System.Reflection"),
                    RoslynReference.FromName("System.ComponentModel.Annotations"),
                    RoslynReference.FromName("System.ComponentModel.DataAnnotations"),
                    RoslynReference.FromName("System.Linq"),
                    RoslynReference.FromName("System.Linq.Expressions"), // Roslyn #23573
                    RoslynReference.FromName("Microsoft.CSharp"), // Roslyn #23573
                    RoslynReference.FromName("DotVVM.Framework"),
                    RoslynReference.FromName("DotVVM.Core"),
                    RoslynReference.FromName("DotvvmAcademy.Validation"),
                    RoslynReference.FromName("DotvvmAcademy.Validation.CSharp"),
                    RoslynReference.FromName("DotvvmAcademy.Validation.Dothtml"),
                    RoslynReference.FromName("DotvvmAcademy.Meta"))
                .WithFilePath(scriptPath)
                .WithSourceResolver(new CourseSourceReferenceResolver(CurrentCourse.Path));
            var script = CSharpScript.Create(
                code: scriptText,
                options: scriptOptions);
            var compilation = script.GetCompilation();
            using var memoryStream = new MemoryStream();
            var emitResult = compilation.Emit(memoryStream);
            if (!emitResult.Success)
            {
                var sb = new StringBuilder($"Compilation of a CodeTask at '{scriptPath}' failed with the following diagnostics:\n");
                sb.Append(string.Join(",\n", emitResult.Diagnostics));
                throw new InvalidOperationException(sb.ToString());
            }
            var entryPoint = compilation.GetEntryPoint(default);
            return new ValidationScript(
                entryType: entryPoint.ContainingType.MetadataName,
                entryMethod: entryPoint.MetadataName,
                bytes: memoryStream.ToArray());
        }

        public async Task<IEnumerable<CodeTaskDiagnostic>> ValidateCodeTask(
            CodeTask codeTask,
            string code)
        {
            var validationId = Guid.NewGuid();

            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var args = new List<string>
            {
                Path.Combine(directory, SandboxPath)
            };

            var script = await GetValidationScript(codeTask.Path);
            FileStream scriptAssemblyFileStream;
            MemoryMappedFile scriptAssemblyMap;
            {
                var mapName = $"CodeTask_{validationId}";
                var mapPath = Path.Combine(Path.GetTempPath(), mapName);
                scriptAssemblyFileStream = new FileStream(mapPath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                scriptAssemblyMap = MemoryMappedFile.CreateFromFile(
                    fileStream: scriptAssemblyFileStream,
                    mapName: null,
                    capacity: script.Bytes.Length,
                    access: MemoryMappedFileAccess.ReadWrite,
                    inheritability: HandleInheritability.None, // sandbox creates its own
                    leaveOpen: true);
                using var mapStream = scriptAssemblyMap.CreateViewStream(
                    offset: 0,
                    size: 0,
                    access: MemoryMappedFileAccess.ReadWrite);
                await mapStream.WriteAsync(script.Bytes, 0, script.Bytes.Length);
                args.Add(mapPath);
                args.Add(script.EntryType);
                args.Add(script.EntryMethod);
            }
            args.Add(CultureInfo.CurrentCulture.Name);
            args.AddRange(codeTask.Dependencies);

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
                var lightDiagnostics = await JsonSerializer.DeserializeAsync<LightDiagnostic[]>(
                    process.StandardOutput.BaseStream);
                // TODO: This Distinct call is a hack
                foreach (var lightDiagnostic in lightDiagnostics.Distinct())
                {
                    if (lightDiagnostic.Source == null || lightDiagnostic.Source.StartsWith("UserCode"))
                    {
                        diagnostics.Add(new CodeTaskDiagnostic(
                            message: lightDiagnostic.Message,
                            start: lightDiagnostic.Start,
                            end: lightDiagnostic.End,
                            severity: lightDiagnostic.Severity.ToCodeTaskDiagnosticSeverity()));
                    }
                }
            }
            catch (JsonException)
            {
                if (!killed)
                {
                    diagnostics.Add(new CodeTaskDiagnostic("Your code could not be validated.", -1, -1, CodeTaskDiagnosticSeverity.Error));
                }
            }
            finally
            {
                scriptAssemblyFileStream.Dispose();
                scriptAssemblyMap.Dispose();
            }
            return diagnostics.ToImmutable();
        }

        internal static string GetAbsolutePath(string root, string origin, string path)
        {
            if (path == null)
            {
                return null;
            }

            if (Path.IsPathRooted(path))
            {
                return path;
            }
            var stop = Path.GetDirectoryName(root);
            while (origin != stop || origin == null)
            {
                var absolutePath = Path.Combine(origin, path);
                if (File.Exists(absolutePath) || Directory.Exists(absolutePath))
                {
                    return absolutePath;
                }
                origin = Path.GetDirectoryName(origin);

            }
            throw new InvalidOperationException($"Path \"{path}\" could not be resolved.");
        }

        private void Cache(string key, object value)
        {
            using var entry = cache.CreateEntry(key);
            entry.SetAbsoluteExpiration(CacheEntryExpiration);
            entry.Value = value;
        }

        private string RenderMarkdown(string markdown)
        {
            var document = Markdown.Parse(markdown, markdigPipeline);
            using var stringWriter = new StringWriter();
            var renderer = new HtmlRenderer(stringWriter);
            markdigPipeline.Setup(renderer);
            renderer.Render(document);
            return stringWriter.ToString();
        }

        private async Task<string> ReadFile(string filePath)
        {
            using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None, 4096, true);
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }

        private async Task<TFrontMatter> ParseFrontMatter<TFrontMatter>(string filePath)
            where TFrontMatter : new()
        {
            var document = Markdown.Parse(await ReadFile(filePath), markdigPipeline);
            if (document.Count == 0 || !(document[0] is YamlFrontMatterBlock markdownBlock))
            {
                throw new ArgumentException($"Step at \"{filePath}\" does not contain a YAML Front Matter.");
            }
            try
            {
                return yamlDeserializer.Deserialize<TFrontMatter>(markdownBlock.Lines.ToString());
            }
            catch(YamlException exception)
            {
                throw new InvalidOperationException($"An exception occured while parsing the front matter of \"{filePath}\".", exception);
            }
        }

        private static ImmutableArray<string> ResolveDependencies(string root, string origin, IEnumerable<string> dependencies)
        {
            if (dependencies == null)
            {
                return ImmutableArray.Create<string>();
            }
            else
            {
                return dependencies.Select(d => GetAbsolutePath(root, origin, d))
                    .ToImmutableArray();
            }
        }
    }
}
