namespace DotvvmAcademy.DAL.Entities
{
    public class Step : IEntity
    {
        public string Path { get; set; }

        public IStepPart[] Source { get; set; }
    }
}