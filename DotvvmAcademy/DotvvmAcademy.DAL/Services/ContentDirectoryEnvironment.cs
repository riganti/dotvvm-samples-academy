using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace DotvvmAcademy.DAL.Services
{
    public class ContentDirectoryEnvironment
    {
        public ContentDirectoryEnvironment(IHostingEnvironment hostingEnvironment)
        {
            IsDevelopment = hostingEnvironment.IsDevelopment();
            var contentPath = Path.Combine(hostingEnvironment.ContentRootPath, ContentConstants.ContentDirectory);
            var lessonsPath = Path.Combine(contentPath, ContentConstants.LessonsDirectory);
            var validatorsPath = Path.Combine(contentPath, ContentConstants.ValidatorsDirectory);
            ContentDirectory = new DirectoryInfo(contentPath);
            LessonsDirectory = new DirectoryInfo(lessonsPath);
            ValidatorsDirectory = new DirectoryInfo(validatorsPath);
        }

        public bool IsDevelopment { get; set; }

        public DirectoryInfo ContentDirectory { get; }

        public DirectoryInfo LessonsDirectory { get; }

        public DirectoryInfo ValidatorsDirectory { get; }

        /// <summary>
        /// From the passed absolute path makes a new path relative to the 'Content' directory.
        /// </summary>
        /// <returns></returns>
        public string GetRelative(string absolute)
        {
            var uriString = new Uri(ContentDirectory.FullName).MakeRelativeUri(new Uri(absolute)).OriginalString;
            var contentNameLength = ContentConstants.ContentDirectory.Length;
            return '.' + uriString.Substring(contentNameLength, uriString.Length - contentNameLength);
        }

        public string GetAbsolute(string relative)
        {
            return Path.Combine(ContentDirectory.FullName, relative);
        }
    }
}