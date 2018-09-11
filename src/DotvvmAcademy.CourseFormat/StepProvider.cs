﻿using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public class StepProvider : ISourceProvider<Step>
    {
        private readonly ICourseEnvironment environment;
        private readonly IMarkdownRenderer renderer;

        public StepProvider(ICourseEnvironment environment, IMarkdownRenderer renderer)
        {
            this.environment = environment;
            this.renderer = renderer;
        }

        public async Task<Step> Get(string path)
        {
            var file = (await environment.GetFiles(path))
                .Single(f => f.EndsWith(".md"));
            using (var stream = environment.OpenRead(file))
            using (var reader = new StreamReader(stream))
            {
                var fileText = await reader.ReadToEndAsync();
                (var html, var frontMatter) = await renderer.Render<StepFrontMatter>(fileText);
                return new Step(
                    path: path,
                    text: html,
                    name: frontMatter.Title,
                    codeTaskPath: frontMatter.CodeTask);
            }
        }
    }
}