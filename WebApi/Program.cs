using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) {
			string ipAddtess = "http://localhost:5001/";
            if(File.Exists(Environment.CurrentDirectory + "\\ipAddress.txt"))
			    using (StreamReader sr = File.OpenText(Environment.CurrentDirectory+"\\ipAddress.txt"))
			    {
				    ipAddtess = sr.ReadLine();
			    }
			
			return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls(ipAddtess);
		}
    }
}
