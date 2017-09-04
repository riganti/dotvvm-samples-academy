using DotvvmAcademy.DAL.Base.Entities;
using DotvvmAcademy.DAL.Base.Providers;
using System;
using System.Linq;

namespace DotvvmAcademy.DAL.FileSystem.Providers
{
    public class FileSystemValidatorProvider : IValidatorProvider
    {
        public IQueryable<IValidator> GetQueryable()
        {
            throw new NotImplementedException();
        }
    }
}