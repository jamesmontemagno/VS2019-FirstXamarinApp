using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyFirstMobileApp.Services;
using MyFirstMobileApp.Shared.Models;
using MyFirstMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Xamarin.Essentials;

namespace MyFirstMobileApp
{
    public class Startup
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public static void Init()
        {
            var configLocation = ExtractAppSettings("MyFirstMobileApp.appsettings.json");

            var host = new HostBuilder()
            #region HostConfiguration
                .ConfigureHostConfiguration(c =>
                {
                    c.AddCommandLine(new string[] { $"ContentRoot={FileSystem.AppDataDirectory}" });
                    c.AddJsonFile(configLocation);
                })
                .ConfigureServices((c, x) => ConfigureServices(c, x))
            #endregion
            #region Logging
                .ConfigureLogging(l => l.AddConsole(o =>
                {
                    o.DisableColors = true;
                }))
            #endregion
                .Build();

            ServiceProvider = host.Services;
        }

        static void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            var world = ctx.Configuration["Hello"];

            services.AddHttpClient();

            if (ctx.HostingEnvironment.IsDevelopment())
                services.AddSingleton<IDataStore<Item>, MockDataStore>();
            else
                services.AddSingleton<IDataStore<Item>, AzureDataStore>();

            services.AddTransient<ItemsViewModel>();
        }

        public static string ExtractAppSettings(string filename)
        {
            var location = FileSystem.CacheDirectory;
            string full = null;
            var a = Assembly.GetExecutingAssembly();
            using (var resFilestream = a.GetManifestResourceStream(filename))
            {
                if (resFilestream != null)
                {
                    full = Path.Combine(location, filename);

                    using (var stream = File.Create(full))
                    {
                        resFilestream.CopyTo(stream);
                    }
                }
            }

            return full;
        }
    }
}
