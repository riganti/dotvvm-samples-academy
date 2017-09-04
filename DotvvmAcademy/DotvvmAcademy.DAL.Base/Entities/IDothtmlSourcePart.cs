namespace DotvvmAcademy.DAL.Base.Entities
{
    public interface IDothtmlSourcePart : IBasicSampleSourcePart
    {
        ISample MasterPage { get; }

        ISample ViewModel { get; }
    }
}