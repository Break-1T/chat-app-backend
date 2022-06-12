using Chat.Api.Interfaces;
using Chat.Api.Services;

namespace Chat.Api.Extensions
{
    public static class ApiServicesCollectionExtensions
    {
        public static void AddApiServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
