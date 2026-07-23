using AppDoMAUI.Services;
using AppDoMAUI.Views;
using Microsoft.Extensions.Logging;

namespace AppDoMAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddHttpClient<AuthService>(client =>
            {
                client.BaseAddress = new Uri(
                    "https://localhost:7230/");
            });

            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<RegisterPage>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
