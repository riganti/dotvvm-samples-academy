using System.Collections.Generic;

namespace DotvvmAcademy.DAL.Base.Entities
{
    public class ValidatorAssembly : IEntity
    {
        public int Id { get; set; }

        public List<Validator> Validators { get; set; }

        public string AssemblyPath { get; set; }
    }
}