using CommunityToolkit.Maui;
using MAUIApp.Example.Models;
using MAUIApp.Example.Services.EmployeeAppService;
using MAUIApp.Example.Services.HttpAppService;
using MAUIApp.Example.Services.LoginAppService;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace MAUIApp.Example
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>().UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<ILoginAppService>(new LoginAppService());
            builder.Services.AddTransient<IEmployeeAppService, EmployeeAppService>();
            builder.Services.AddSingleton<IHttpAppService, HttpAppService>();

            return builder.Build();
        }
    }
}
