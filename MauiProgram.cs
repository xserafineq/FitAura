using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace FitAura
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
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddMudServices();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif



            builder.Services.AddHttpClient<FitAura.Services.ProductService>("FitAuraApi", client =>
               {
                   client.BaseAddress = new Uri("https://localhost:7017/");
               });

            builder.Services.AddHttpClient<FitAura.Services.MealService>("FitAuraApi", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7017/");
            });

            builder.Services.AddHttpClient<FitAura.Services.UserService>("FitAuraApi", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7017/");
            })
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                var handler = new HttpClientHandler();
#if DEBUG
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true;
#endif
                return handler;
            });

            builder.Services.AddHttpClient<FitAura.Services.DayService>("FitAuraApi", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7017/");
            });

            builder.Services.AddHttpClient<FitAura.Services.MeasurementService>("FitAuraApi", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7017/");
            });

            builder.Services.AddScoped<UserState>();
            builder.Services.AddScoped<DayState>();

            return builder.Build();
        }
    }
}