using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Threading;

namespace DotvvmAcademy.CourseFormat.Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = MemoryMappedFile.OpenExisting("academy", MemoryMappedFileRights.ReadWrite);
            using (file)
            {
                using (var stream = file.CreateViewStream(0, 0, MemoryMappedFileAccess.ReadWrite))
                using (var writer = new StreamWriter(stream))
                {
                    Console.WriteLine("Hello, DotVVM Academy!");
                    writer.WriteLine("Hello, DotVVM Academy!");
                }
            }
        }
    }
}
