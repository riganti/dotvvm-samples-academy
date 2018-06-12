using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DotvvmAcademy.CourseFormat
{
    internal class CourseWorkspaceVisitor
    {

        public Dictionary<string, CodeTaskId> CodeTasks {get;}
            = new Dictionary<string, CodeTaskId>();

        public Dictionary<string, LessonId> Lessons {get;}
            = new Dictionary<string, LessonId>();

        public Dictionary<string, StepId> Steps {get;}
            = new Dictionary<string, StepId>();

        public Dictionary<string, CourseVariantId> Variants {get;}
            = new Dictionary<string, CourseVariantId>();

        public void VisitRoot(DirectoryInfo root)
        {
            foreach(var variant in root.EnumerateDirectories())
            {
                VisitVariant(variant);
            }
        }

        public void VisitVariant(DirectoryInfo variant)
        {
            var id = new CourseVariantId(variant.Name);
            Variants[id.Path] = id;
            foreach(var lesson in variant.EnumerateDirectories())
            {
                VisitLesson(id, lesson);
            }
        }

        public void VisitLesson(CourseVariantId variant, DirectoryInfo lesson)
        {
            var id = new LessonId(variant, lesson.Name);
            Lessons[id.Path] = id;
            foreach(var step in lesson.EnumerateDirectories())
            {
                VisitStep(id, step);
            }
        }

        public void VisitStep(LessonId lesson, DirectoryInfo step)
        {
            var id = new StepId(lesson, step.Name);
            Steps[id.Path] = id;
            VisitCodeTask(id, step);
        }

        public void VisitCodeTask(StepId step, DirectoryInfo codeTask)
        {
            var codeFile = codeTask
                .EnumerateFiles("code_*.*", SearchOption.TopDirectoryOnly)
                .FirstOrDefault();
            var scriptFile = codeTask
                .EnumerateFiles("validate_*.csx", SearchOption.TopDirectoryOnly)
                .FirstOrDefault();
            if (codeFile == null || scriptFile == null) {
                return;
            }
            var id = new CodeTaskId(step, codeFile.Name, scriptFile.Name);
            CodeTasks[id.Path] = id;
        }
    }
}