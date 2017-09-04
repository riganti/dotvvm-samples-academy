using System.Collections.Generic;

namespace DotvvmAcademy.DAL.Base.Entities
{
    public interface IValidatorAssembly : IEntity
    {
        string AssemblyPath { get; }
    }
}