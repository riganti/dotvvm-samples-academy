using DotvvmAcademy.DAL.Base.Entities;
using DotvvmAcademy.DAL.Base.Providers;
using System;
using System.Linq;

namespace DotvvmAcademy.DAL.FileSystem.Providers
{
    public class FileSystemValidatorAssemblyProvider : IValidatorAssemblyProvider
    {
        public IQueryable<IValidatorAssembly> GetQueryable()
        {
            throw new NotImplementedException();
        }
    }
}