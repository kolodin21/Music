using Microsoft.Extensions.DependencyInjection;
using Music.Application.Entity.Albums;
using Music.Application.Entity.Artists;
using Music.Application.Entity.Songs;

namespace Music.Application;

public static class ServiceApplicationCollection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAlbumsService, AlbumsService>();
        services.AddScoped<IArtistsService, ArtistsService>();
        services.AddScoped<ISongsService, SongsService>();

        return services;
    }
}