using DotvvmAcademy.DAL.Base.Entities;
using DotvvmAcademy.DAL.Base.Providers;
using System;
using System.Linq;

namespace DotvvmAcademy.DAL.FileSystem.Providers
{
    public class FileSystemStepProvider : IStepProvider
    {
        public IQueryable<IStep> GetQueryable()
        {
            throw new NotImplementedException();
        }
    }
}