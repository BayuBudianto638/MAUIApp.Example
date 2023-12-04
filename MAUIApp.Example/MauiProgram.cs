using CommunityToolkit.Maui;
using MAUIApp.Example.Services.HttpAppService;
using MAUIApp.Example.Services.LoginAppService;
using Microsoft.Extensions.Logging;

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

            builder.Services.AddSingleton<ILoginAppService>(new LoginAppService("EndPoint"));
            builder.Services.AddSingleton<IHttpAppService, HttpAppService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
