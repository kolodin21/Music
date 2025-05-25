using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Music.Application.Albums;
using Music.Application.Artists;
using Music.Application.Songs;
using Music.Infrastructure.SQLite.Repositories;

namespace Music.Infrastructure.SQLite.Configurations;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDbContextSqLite(this IServiceCollection services, string? connection)
    {
        services.AddDbContext<MusicDbContext>(options =>
            options.UseSqlite(connection));
        return services;
    }


    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAlbumRepository, AlbumRepository>();
        services.AddScoped<IArtistRepository, ArtistRepository>();
        services.AddScoped<ISongRepository, SongRepository>();
        return services;
    }

}


