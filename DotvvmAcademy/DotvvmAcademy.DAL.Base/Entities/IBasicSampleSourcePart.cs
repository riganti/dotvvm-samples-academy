namespace DotvvmAcademy.DAL.Base.Entities
{
    public interface IBasicSampleSourcePart : ISourcePart
    {
        ISample Correct { get; }

        ISample Incorrect { get; }

        string ValidatorId { get; }
    }
}