namespace DotvvmAcademy.DAL.Base.Entities
{
    public abstract class BasicSampleSourcePart : SourcePart
    {
        public Sample Correct { get; set; }

        public Sample Incorrect { get; set; }

        public Validator Validator { get; set; }
    }
}