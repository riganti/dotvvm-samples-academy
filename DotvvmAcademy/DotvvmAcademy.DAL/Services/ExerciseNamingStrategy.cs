using Newtonsoft.Json.Serialization;

namespace DotvvmAcademy.DAL.Services
{
    public class ExerciseNamingStrategy : NamingStrategy
    {
        protected override string ResolvePropertyName(string name)
        {
            return $"@{name}";
        }
    }
}