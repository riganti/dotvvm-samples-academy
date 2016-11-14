using DotVVM.Framework.ViewModel;
using System;
using System.Collections.Generic;
using DotvvmAcademy.Lessons;

namespace DotvvmAcademy.Steps
{
    public abstract class StepBase 
    {
        public StepBase(LessonBase currentLesson)
        {
            CurrentLesson = currentLesson;
        }
        protected LessonBase CurrentLesson { get; set; }

        public int StepIndex { get; set; }

        [Bind(Direction.ServerToClient)]
        public string Title { get; set; }

        [Bind(Direction.ServerToClient)]
        public string Description { get; set; }
        
        public string ErrorMessage => string.Join(" ", GetErrors());

        protected abstract IEnumerable<string> GetErrors();
    }
}