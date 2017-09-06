using AutoMapper;
using DotvvmAcademy.DAL.Services;
using System.IO;

namespace DotvvmAcademy.DAL.Profiles
{
    public class FileSystemInfoProfile : Profile
    {
        public FileSystemInfoProfile(ContentDirectoryEnvironment environment)
        {
            CreateMap<FileSystemInfo, string>()
                .ConstructUsing(f => environment.GetRelative(f.FullName));
        }
    }
}