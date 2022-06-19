using Chat.Api.Interfaces;
using Chat.Api.Options;
using Chat.Api.Services;

namespace Chat.Api.Extensions
{
    public static class ApiServicesCollectionExtensions
    {
        private const string AuthSecrion = "Auth";

        public static void AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IUserService, UserService>();
            //services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGroupService, GroupService>();

            services.Configure<AuthOptions>(configuration.GetSection(AuthSecrion));
        }
    }
}
