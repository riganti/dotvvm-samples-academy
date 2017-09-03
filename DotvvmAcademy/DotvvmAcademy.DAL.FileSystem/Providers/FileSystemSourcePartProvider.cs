using DotvvmAcademy.DAL.Base.Entities;
using DotvvmAcademy.DAL.Base.Providers;
using System;
using System.Linq;

namespace DotvvmAcademy.DAL.FileSystem.Providers
{
    public class FileSystemSourcePartProvider<TSourcePart> : ISourcePartProvider<TSourcePart>
        where TSourcePart : SourcePart, new()
    {
        public IQueryable<TSourcePart> GetQueryable()
        {
            throw new NotImplementedException();
        }
    }
}