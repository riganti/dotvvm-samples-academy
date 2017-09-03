using DotvvmAcademy.DAL.Base.Entities;
using DotvvmAcademy.DAL.Base.Providers;
using System;
using System.Linq;

namespace DotvvmAcademy.DAL.FileSystem.Providers
{
    public class FileSystemValidatorAssemblyProvider : IValidatorAssemblyProvider
    {
        public IQueryable<ValidatorAssembly> GetQueryable()
        {
            throw new NotImplementedException();
        }
    }
}