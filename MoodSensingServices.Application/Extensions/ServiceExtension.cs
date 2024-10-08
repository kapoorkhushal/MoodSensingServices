using Microsoft.Extensions.DependencyInjection;
using MoodSensingServices.Application.BusinessLogic;
using MoodSensingServices.Application.Handler;
using ServiceProviders.Application.Features;

namespace MoodSensingServices.Application.Extensions
{
    public static class ServiceExtension
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<HappyLocationHandler>();
            services.AddScoped<MoodFrequencyHandler>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IMoodOperationService, MoodOperationService>();
        }
    }
}
