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

            builder.Services.AddHttpClient();
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddMudServices();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            var baseUri = new Uri(Config.ApiUrl);

            static HttpClientHandler GetInsecureHandler()
            {
                var handler = new HttpClientHandler();
#if DEBUG
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true;
#endif
                return handler;
            }

            builder.Services.AddHttpClient<FitAura.Services.ProductService>("FitAuraApi", client => client.BaseAddress = baseUri)
                .ConfigurePrimaryHttpMessageHandler(GetInsecureHandler);

            builder.Services.AddHttpClient<FitAura.Services.MealService>("FitAuraApi", client => client.BaseAddress = baseUri)
                .ConfigurePrimaryHttpMessageHandler(GetInsecureHandler);

            builder.Services.AddHttpClient<FitAura.Services.UserService>("FitAuraApi", client => client.BaseAddress = baseUri)
                .ConfigurePrimaryHttpMessageHandler(GetInsecureHandler);

            builder.Services.AddHttpClient<FitAura.Services.DayService>("FitAuraApi", client => client.BaseAddress = baseUri)
                .ConfigurePrimaryHttpMessageHandler(GetInsecureHandler);

            builder.Services.AddHttpClient<FitAura.Services.MeasurementService>("FitAuraApi", client => client.BaseAddress = baseUri)
                .ConfigurePrimaryHttpMessageHandler(GetInsecureHandler);

            builder.Services.AddHttpClient<FitAura.Services.RecipeService>("FitAuraApi", client => client.BaseAddress = baseUri)
                .ConfigurePrimaryHttpMessageHandler(GetInsecureHandler);
            
            builder.Services.AddHttpClient<FitAura.Services.CategoryService>("FitAuraApi", client => client.BaseAddress = baseUri)
                .ConfigurePrimaryHttpMessageHandler(GetInsecureHandler);

            builder.Services.AddScoped<UserState>();
            builder.Services.AddScoped<DayState>();

            return builder.Build();
        }
    }
}