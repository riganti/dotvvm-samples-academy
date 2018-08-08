namespace DotvvmAcademy.Validation.Unit
{
    public interface IQueryFactory
    {
        IQuery<TResult> Create<TResult>(string queryString);
    }
}