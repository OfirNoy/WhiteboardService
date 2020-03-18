using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using WhiteboardService.Logic;

namespace WhiteboardService
{
    public class Program
    {   
        public static void Main(string[] args)
        {            
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<WebApp>();
                });


        private static void TestStuff()
        {
        }
    }
}
