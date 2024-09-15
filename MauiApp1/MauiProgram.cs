using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models;
using CommunityToolkit.Maui;
using UraniumUI;
using Constants = Models.Constants;

namespace InvetorySystem
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
                .UseMauiCommunityToolkit()
                .UseUraniumUI()
                .UseUraniumUIMaterial();
#if DEBUG
            builder.Services.AddDbContext<InventroyDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString(Constants.DatabasePath)));
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}