using DotvvmAcademy.DAL.Base.FileSystem;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace DotvvmAcademy.DAL.FileSystem.Services
{
    public class ContentDirectoryEnvironment
    {
        public ContentDirectoryEnvironment(IHostingEnvironment hostingEnvironment)
        {
            ContentDirectoryPath = Path.Combine(hostingEnvironment.ContentRootPath, ContentConstants.ContentDirectory);
            LessonsDirectoryPath = Path.Combine(ContentDirectoryPath, ContentConstants.LessonsDirectory);
            ValidatorsDirectoryPath = Path.Combine(ContentDirectoryPath, ContentConstants.ValidatorsDirectory);
        }

        public string ContentDirectoryPath { get; }

        public string LessonsDirectoryPath { get; }

        public string ValidatorsDirectoryPath { get; }
    }
}