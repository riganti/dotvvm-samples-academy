using System;
using System.Collections.Generic;

namespace DotvvmAcademy.DAL.Base.Models
{
    public class Lesson
    {
        public Lesson(int index, string language)
        {
            Index = index;
            Language = language;
        }

        public string Annotation { get; set; }

        public string ConfigPath { get; set; }

        public string DirectoryPath { get; set; }

        public string ImageUrl { get; set; }

        public bool IsReady { get; set; }

        public string Language { get; }

        public string Name { get; set; }

        public int Index { get; }

        /// <summary>
        /// The file paths pointing towards individual markdown step pages
        /// relative to the directory the configuration file is in.
        /// </summary>
        public List<string> Steps { get; set; }
    }
}