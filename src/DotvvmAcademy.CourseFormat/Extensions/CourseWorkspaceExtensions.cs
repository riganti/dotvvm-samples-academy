using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public static class CourseWorkspaceExtensions
    {
        public static Task<Lesson> LoadLesson(this CourseWorkspace workspace, string variant, string lesson)
        {
            return workspace.Load<Lesson>($"/{CourseConstants.ContentDirectory}/{variant}/{lesson}");
        }

        public static Task<Root> LoadRoot(this CourseWorkspace workspace)
        {
            return workspace.Load<Root>("/");
        }

        public static Task<Step> LoadStep(this CourseWorkspace workspace, string variant, string lesson, string step)
        {
            return workspace.Load<Step>($"/{CourseConstants.ContentDirectory}/{variant}/{lesson}/{step}");
        }

        public static Task<Variant> LoadVariant(this CourseWorkspace workspace, string variant)
        {
            return workspace.Load<Variant>($"/{CourseConstants.ContentDirectory}/{variant}");
        }

        public static async Task<TSource> Require<TSource>(this CourseWorkspace workspace, string sourcePath)
            where TSource : Source
        {
            var source = await workspace.Load<TSource>(sourcePath);
            if (source == null)
            {
                throw new ArgumentException($"Required {nameof(Source)} '{sourcePath}' does not exist.", nameof(sourcePath));
            }

            return source;
        }
    }
}