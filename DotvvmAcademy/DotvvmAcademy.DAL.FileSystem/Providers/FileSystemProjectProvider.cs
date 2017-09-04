using DotvvmAcademy.DAL.Base.Entities;
using DotvvmAcademy.DAL.Base.Providers;
using System;
using System.Linq;

namespace DotvvmAcademy.DAL.FileSystem.Providers
{
    public class FileSystemProjectProvider : IProjectProvider
    {
        public IQueryable<IProject> GetQueryable()
        {
            throw new NotImplementedException();
        }
    }
}