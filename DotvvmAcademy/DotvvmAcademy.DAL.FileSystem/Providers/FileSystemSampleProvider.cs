using DotvvmAcademy.DAL.Base.Entities;
using DotvvmAcademy.DAL.Base.Providers;
using System;
using System.Linq;

namespace DotvvmAcademy.DAL.FileSystem.Providers
{
    public class FileSystemSampleProvider : ISampleProvider
    {
        public IQueryable<Sample> GetQueryable()
        {
            throw new NotImplementedException();
        }
    }
}