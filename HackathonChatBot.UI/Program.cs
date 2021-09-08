using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace HackathonChatBot.UI
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
                    webBuilder.UseStartup<Startup>();
#if !DEBUG
                    webBuilder.UseUrls("https://localhost:5321/", "http://localhost:5320/");
#endif
                });
    }
}
