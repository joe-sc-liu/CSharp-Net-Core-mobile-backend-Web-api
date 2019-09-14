using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using log4net;
using log4net.Config;
using System.Reflection;

namespace AppWebApi
{
    public class Program
    {
        private readonly static ILog logger = LogManager.GetLogger(typeof(Program));

        public static void Main(string[] args)
        {
            LoadLog4netConfig();
            logger.Info("Application Start");

            try
            {
                //.UseUrls("http://*:5000")
                //                .UseKestrel()
                //.UseContentRoot(Directory.GetCurrentDirectory())
                //.UseIISIntegration()
                //.UseStartup<Startup>()


                CreateWebHostBuilder(args)
                .Build()
                .Run();
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }

        private static void LoadLog4netConfig()
        {
            var repository = LogManager.CreateRepository(
                    Assembly.GetEntryAssembly(),
                    typeof(log4net.Repository.Hierarchy.Hierarchy)
                );
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
