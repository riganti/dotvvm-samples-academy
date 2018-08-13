using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public static class CourseWorkspaceExtensions
    {
        public static async Task<TSource> Load<TSource>(this CourseWorkspace workspace, string sourcePath)
            where TSource : Source
        {
            var source = await workspace.Load(sourcePath);
            if (source == null)
            {
                return null;
            }

            if (source is TSource cast)
            {
                return cast;
            }
            else
            {
                throw new ArgumentException($"Path doesn't point to a '{typeof(TSource)}'.", nameof(sourcePath));
            }
        }

        public static Task<Lesson> LoadLesson(this CourseWorkspace workspace, string variant, string lesson)
        {
            return workspace.Load<Lesson>($"/{CourseEnvironment.ContentDirectory}/{variant}/{lesson}");
        }

        public static Task<WorkspaceRoot> LoadRoot(this CourseWorkspace workspace)
        {
            return workspace.Load<WorkspaceRoot>("/");
        }

        public static Task<Step> LoadStep(this CourseWorkspace workspace, string variant, string lesson, string step)
        {
            return workspace.Load<Step>($"/{CourseEnvironment.ContentDirectory}/{variant}/{lesson}/{step}");
        }

        public static Task<CourseVariant> LoadVariant(this CourseWorkspace workspace, string variant)
        {
            return workspace.Load<CourseVariant>($"/{CourseEnvironment.ContentDirectory}/{variant}");
        }

        public static async Task<Source> Require(this CourseWorkspace workspace, string sourcePath)
        {
            var source = await workspace.Load(sourcePath);
            if (source == null)
            {
                throw new ArgumentException($"Required {nameof(Source)} '{sourcePath}' does not exist.", nameof(sourcePath));
            }

            return source;
        }

        public static async Task<TSource> Require<TSource>(this CourseWorkspace workspace, string sourcePath)
            where TSource : Source
        {
            var source = await workspace.Require(sourcePath);
            if (source is TSource cast)
            {
                return cast;
            }
            else
            {
                throw new ArgumentException($"Source path '{sourcePath}' doesn't point to a '{typeof(TSource)}'.", nameof(sourcePath));
            }
        }
    }
}