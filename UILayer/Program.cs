using Microsoft.AspNetCore;
using AutoMapper;

namespace UILayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) => WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .Build();
    }
}