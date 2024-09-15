using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using ManagementSystem.ViewModels;
using UraniumUI;
using ApiService;
using ManagementSystem.Views;
using DataStore.Webapi;
using Serilog;

namespace ManagementSystem
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseUraniumUI()
                .UseUraniumUIMaterial()
                .UseMauiCommunityToolkit()            
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

           
#if DEBUG
            builder.Logging.AddDebug();
#endif
            IServiceCollection services = builder.Services;
            services.AddSerilog(
                new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(Path.Combine(FileSystem.Current.AppDataDirectory,"log.txt"),rollingInterval:RollingInterval.Day)
                .CreateLogger());
            builder.Services.AddSingleton<IServiceGeneric, DataStoreWebApi>();
            //pages
            builder.Services.AddSingleton<LocationMainView>();
            builder.Services.AddTransient<NewEditLocation>();
            builder.Services.AddTransient<NewAddpage>();
            builder.Services.AddSingleton<R2MainPage>();
            builder.Services.AddSingleton<R1MainPage>();
            builder.Services.AddTransient<ItemAddView>();
            builder.Services.AddTransient<ItemEditView>();
            builder.Services.AddSingleton<LoadingView>();
            builder.Services.AddSingleton<LoginView>();


            //ContentView

            //view model
            builder.Services.AddSingleton<LocationMainViewModel>();
            builder.Services.AddTransient<LocationViewModel>();
            builder.Services.AddTransient<ItemViewModel>();
            builder.Services.AddSingleton<LoadingViewModel>();
            builder.Services.AddSingleton<LoginViewModel>();

            builder.Logging.AddDebug();



            return builder.Build();
        }
    }
}