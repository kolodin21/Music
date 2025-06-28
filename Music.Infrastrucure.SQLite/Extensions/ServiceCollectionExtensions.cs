using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Music.Infrastructure.SQLite.Configurations;
using Music.Repository.EfCore.Database;

namespace Music.Infrastructure.SQLite.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDbContextSqLite(this IServiceCollection services, string? connection)
        {
            services.AddDbContext<IMusicDbContext, MusicDbContext>(options =>
                options.UseSqlite(connection));
            return services;
        }
    }
}
