using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Logging;
using MyFirstMobileApp.Models;
using MyFirstMobileApp.Services;
using MyFirstMobileApp.Shared.Models;
using MyFirstMobileApp.ViewModels;
using Polly;
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
            var a = Assembly.GetExecutingAssembly();
            using var stream = a.GetManifestResourceStream("MyFirstMobileApp.appsettings.json");

            var host = new HostBuilder()
                .ConfigureHostConfiguration(c =>
                {
                    c.AddCommandLine(new string[] { $"ContentRoot={FileSystem.AppDataDirectory}" });
                    c.AddJsonStream(stream);
                })
                .ConfigureServices((c, x) => ConfigureServices(c, x))
                .ConfigureLogging(l => l.AddConsole(o =>
                {
                    o.DisableColors = true;
                }))
                .Build();

            ServiceProvider = host.Services;
        }

        static void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            var world = ctx.Configuration["Hello"];

            services.AddHttpClient("AzureWebsites", client => 
                {
                    client.BaseAddress = new Uri(App.AzureBackendUrl);
                })
                .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10)
                }));

            if (ctx.HostingEnvironment.IsDevelopment())
                services.AddSingleton<IRepository<Item>, MockDataStore>();
            else
                services.AddSingleton<IRepository<Item>, AzureDataStore>();

            services.AddTransient<ItemsViewModel>();
        }
    }
}
