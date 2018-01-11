using System.Collections.Generic;

namespace DotvvmAcademy.DAL.Entities
{
    public class MvvmExerciseStepPart : IStepPart
    {
        public ViewExercise View { get; set; }

        public ViewModelExercise ViewModel { get; set; }

        public class ViewExercise : ExerciseBase
        {
            public string MasterPagePath { get; set; }
        }

        public class ViewModelExercise : ExerciseBase
        {
            public IEnumerable<string> DependencyPaths { get; set; }
        }
    }
}