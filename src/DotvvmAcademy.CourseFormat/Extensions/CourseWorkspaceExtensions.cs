using System;
using System.Threading.Tasks;

namespace DotvvmAcademy.CourseFormat
{
    public static class CourseWorkspaceExtensions
    {
        public static Task<Course> LoadCourse(this CourseWorkspace workspace)
        {
            return workspace.Load<Course>("/");
        }

        public static Task<Lesson> LoadLesson(this CourseWorkspace workspace, string lessonMoniker)
        {
            return workspace.Load<Lesson>($"/{lessonMoniker}");
        }

        public static Task<LessonVariant> LoadLessonVariant(this CourseWorkspace workspace, string lessonMoniker, string variantMoniker)
        {
            return workspace.Load<LessonVariant>($"/{lessonMoniker}/{variantMoniker}");
        }

        public static Task<Step> LoadStep(this CourseWorkspace workspace, string lessonMoniker, string variantMoniker, string stepMoniker)
        {
            return workspace.Load<Step>($"/{lessonMoniker}/{variantMoniker}/{stepMoniker}");
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