using DotVVM.Framework.ViewModel;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Reflection;
using System.Threading.Tasks;

namespace DotvvmAcademy.Web.Pages.Test
{
    public class TestViewModel : DotvvmViewModelBase
    {
        public string Message { get; set; }

        public override Task PreRender()
        {
            var file = MemoryMappedFile.CreateNew(
                mapName: "academy",
                capacity: 1024,
                access: MemoryMappedFileAccess.ReadWrite,
                options: MemoryMappedFileOptions.None,
                inheritability: HandleInheritability.Inheritable);
            using (file)
            {
                using (var stream = file.CreateViewStream(0, 0, MemoryMappedFileAccess.ReadWrite))
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write("Hello, Memory Mapped Files!");
                }
                var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var info = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = $"{directory}/sandbox/DotvvmAcademy.CourseFormat.Sandbox.dll",
                    UseShellExecute = false,
                    CreateNoWindow = false
                };
                var process = Process.Start(info);
                process.WaitForExit();
                using (var stream = file.CreateViewStream(0, 0, MemoryMappedFileAccess.Read))
                using (var reader = new StreamReader(stream))
                {
                    Message = reader.ReadToEnd();
                }
            }
            return base.PreRender();
        }
    }
}