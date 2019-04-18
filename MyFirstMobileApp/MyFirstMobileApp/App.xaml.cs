using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyFirstMobileApp.Services;
using MyFirstMobileApp.Views;
using Xamarin.Essentials;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyFirstMobileApp.Models;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.IO;

namespace MyFirstMobileApp
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        //To debug on Android emulators run the web backend against .NET Core not IIS
        //If using other emulators besides stock Google images you may need to adjust the IP address
        public static string AzureBackendUrl = "http://myfirstmobileapp-mobileappservice.azurewebsites.net";
            //DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://localhost:5000";

        public static bool UseMockDataStore = false;

        public static IServiceProvider ServiceProvider { get; set; }

        public static void ExtractSaveResource(String filename, String location)
        {
            Assembly a = Assembly.GetExecutingAssembly();
            using (var resFilestream = a.GetManifestResourceStream(filename))
            {
                if (resFilestream != null)
                {
                    var full = Path.Combine(location, filename);

                    using (var stream = File.Create(full))
                    {
                        resFilestream.CopyTo(stream);
                    }

                }
            }
        }
        public App()
        {
            InitializeComponent();

            string systemDir = FileSystem.CacheDirectory;
            ExtractSaveResource("MyFirstMobileApp.appsettings.json", systemDir);
            var fullConfig = Path.Combine(systemDir, "MyFirstMobileApp.appsettings.json");

            var host = new HostBuilder()
                .ConfigureServices((c, x) => ConfigureServices(c, x))
                .ConfigureHostConfiguration(c => c.AddJsonFile(fullConfig))
                .ConfigureLogging(l => l.AddConsole(o =>
                {
                    o.DisableColors = true;
                }))
                .ConfigureAppConfiguration(c => c.AddJsonFile(fullConfig))
                .Build();


            ServiceProvider = host.Services;

            MainPage = new MainPage();
        }

        void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            var world = ctx.Configuration["Hello"];

            services.AddHttpClient();

            if (ctx.HostingEnvironment.IsDevelopment())
                services.AddSingleton<IDataStore<Item>, MockDataStore>();
            else
                services.AddSingleton<IDataStore<Item>, AzureDataStore>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
