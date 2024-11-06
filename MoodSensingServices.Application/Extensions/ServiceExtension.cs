using Microsoft.Extensions.DependencyInjection;
using MoodSensingServices.Application.BusinessLogic;

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

            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IMoodOperationService, MoodOperationService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IUserImageOperationService, UserImageOperationService>();
        }
    }
}
