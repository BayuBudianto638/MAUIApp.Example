using CommunityToolkit.Maui;
using MAUIApp.Example.Models;
using MAUIApp.Example.Services.HttpAppService;
using MAUIApp.Example.Services.LoginAppService;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace MAUIApp.Example
{
    public static class MauiProgram
    {
        private static readonly string ApiUrl = String.Empty;
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

            string jsonConfig = LoadJsonConfig("appconfig.json");

            if (!string.IsNullOrEmpty(jsonConfig))
            {
                AppConfig appConfig = JsonSerializer.Deserialize<AppConfig>(jsonConfig);

                builder.Services.AddSingleton(new AppConfig
                {
                    ApiUrl = appConfig.ApiUrl
                });

            }

            builder.Services.AddSingleton<ILoginAppService>(new LoginAppService(ApiUrl));
            builder.Services.AddSingleton<IHttpAppService, HttpAppService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static string? LoadJsonConfig(string fileName)
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON configuration: {ex.Message}");
                return null;
            }
        }
    }
}
