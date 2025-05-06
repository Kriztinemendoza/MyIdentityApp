using Microsoft.Extensions.Logging;
using MyIdentityApp.Services;
using Plugin.Maui.Audio;

namespace MyIdentityApp
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
                    fonts.AddFont("fa-solid-900.ttf", "FontAwesome"); // FontAwesome icons
                })
                .RegisterServices()
                .ConfigureEffects(effects =>
                {
                    // Add any effects here if needed
                });

            builder.Services.AddMauiBlazorWebView();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
            
            AppLogger.Configure(builder.Logging.Services.BuildServiceProvider().GetRequiredService<ILoggerFactory>());
#endif

            return builder.Build();
        }

        private static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
        {
            // Register services
            builder.Services.AddSingleton<ScriptureService>();
            builder.Services.AddSingleton<BackgroundService>();
            builder.Services.AddSingleton<TtsService>();
            builder.Services.AddSingleton(AudioManager.Current);
            builder.Services.AddSingleton<AudioService>();
            return builder;
        }
    }
}