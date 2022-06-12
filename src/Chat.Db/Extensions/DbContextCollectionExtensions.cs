using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace Chat.Db.Extensions
{
    /// <summary>
    /// DbServiceCollectionExtensions.
    /// </summary>
    public static class DbServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the database services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>IServiceCollection.</returns>
        /// <exception cref="ArgumentNullException">services</exception>
        /// <exception cref="ArgumentException">Connection string must be not null or empty - connectionString</exception>
        public static IServiceCollection AddDbServices(this IServiceCollection services, string connectionString)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("Connection string must be not null or empty", nameof(connectionString));
            }

            services.AddDbContextPool<ChatDbContext>(contextOptions =>
            {
                contextOptions.UseNpgsql(connectionString, npgOptions =>
                {
                    npgOptions.MigrationsAssembly("Chat.Db.Migrations")
                        .EnableRetryOnFailure();
                });
            });

            //services.AddScoped<ISessionStore, SessionStore>();

            return services;
        }
    }
}
