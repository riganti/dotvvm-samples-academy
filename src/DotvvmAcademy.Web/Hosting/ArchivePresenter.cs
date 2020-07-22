﻿using DotVVM.Framework.Hosting;
using DotvvmAcademy.CourseFormat;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace DotvvmAcademy.Web.Hosting
{
    public class ArchivePresenter : IDotvvmPresenter
    {
        private readonly CourseWorkspace workspace;

        public ArchivePresenter(CourseWorkspace workspace)
        {
            this.workspace = workspace;
        }

        public async Task ProcessRequest(IDotvvmRequestContext context)
        {
            (var languageMoniker, var lessonMoniker, var stepMoniker) = GetParameters(context);
            var archive = workspace.CurrentCourse.GetLesson(lessonMoniker)
                .GetVariant(languageMoniker)
                .GetStep(stepMoniker)
                .Archive;
            var bytes = await workspace.GetArchiveBytes(archive);
            context.HttpContext.Response.ContentType = "application/zip";
            var contentDisposition = new ContentDisposition
            {
                FileName = $"{archive.Name}.zip"
            };
            context.HttpContext.Response.Headers["Content-Disposition"] = contentDisposition.ToString();
            await context.HttpContext.Response.Body.WriteAsync(bytes);
        }

        private (string languageMoniker, string lessonMoniker, string stepMoniker) GetParameters(IDotvvmRequestContext context)
        {
            if (!context.Parameters.TryGetValue("Language", out var languageMoniker)
                || !context.Parameters.TryGetValue("Lesson", out var lessonMoniker)
                || !context.Parameters.TryGetValue("Step", out var stepMoniker))
            {
                throw new NotSupportedException("ArchivePresenter cannot be used with this route.");
            }

            return ((string)languageMoniker, (string)lessonMoniker, (string)stepMoniker);
        }
    }
}
