using System.Collections.Generic;

namespace DotvvmAcademy.DAL.Base.Entities
{
    public class MvvmSampleSourcePart : SourcePart
    {
        public Sample CorrectView { get; set; }

        public Sample CorrectViewModel { get; set; }

        public Sample IncorrectView { get; set; }

        public Sample IncorrectViewModel { get; set; }

        public Sample MasterPage { get; set; }

        public List<Sample> ViewModelDependencies { get; set; }

        public Validator ViewModelValidator { get; set; }

        public Validator ViewValidator { get; set; }
    }
}