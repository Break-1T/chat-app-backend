using Chat.Api.Interfaces;
using Chat.Api.Options;
using Chat.Api.Services;

namespace Chat.Api.Extensions
{
    public static class ApiServicesCollectionExtensions
    {
        private const string AuthSection = "Auth";

        public static void AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IUserService, UserService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IChatService, ChatService>();

            services.Configure<AuthOptions>(configuration.GetSection(AuthSection));
        }
    }
}
