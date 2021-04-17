using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSS_MVC
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
                });
    }
}

//"ConnectionStr": "Server=LAPTOP-QAE0FLMJ\\SQLEXPRESS;Database=LibraryDB;Trusted_Connection=True;MultipleActiveResultSets=true"
//"ConnectionStr": "Server=(localdb)\\mssqllocaldb;Database=LibraryDB;Trusted_Connection=True;MultipleActiveResultSets=true"

//"ConnectionStr": "Server=(localdb)\\MSSQLLocalDB;Database=Ario_Assignment4567_LibraryDB;Trusted_Connection=True;MultipleActiveResultSets=true"