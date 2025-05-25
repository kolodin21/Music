using Microsoft.Extensions.DependencyInjection;
using Music.Application.Albums;
using Music.Application.Artists;
using Music.Application.Songs;

namespace Music.Application;

public static class DependencyInjections
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAlbumsService, AlbumsService>();
        services.AddScoped<IArtistsService, ArtistsService>();
        services.AddScoped<ISongsService, SongsService>();

        return services;
    }
}