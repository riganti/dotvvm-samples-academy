namespace DotvvmAcademy.DAL.Base.Entities
{
    public class Validator : IEntity
    {
        public ValidatorAssembly ContainingAssembly { get; set; }

        public int Id { get; set; }

        public string ValidatorKey { get; set; }
    }
}