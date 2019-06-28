using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace DotvvmAcademy.Course.ProfileDetail
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebHost.CreateDefaultBuilder<Startup>(args)
                .Build()
                .Run();
        }
    }
}
