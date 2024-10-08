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
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
            }

            services.AddScoped<HappyLocationHandler>();
            services.AddScoped<MoodFrequencyHandler>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IMoodOperationService, MoodOperationService>();
        }
    }
}
