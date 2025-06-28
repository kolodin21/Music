using Microsoft.Extensions.DependencyInjection;
using Music.Application.Entity.Albums;
using Music.Application.Entity.Artists;
using Music.Application.Entity.Songs;
using Music.Repository.EfCore.Repositories;

namespace Music.Repository.EfCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositoryEfCore(this IServiceCollection services)
        {
            services.AddScoped<IAlbumRepository, AlbumRepository>();
            services.AddScoped<IArtistRepository, ArtistRepository>();
            services.AddScoped<ISongRepository, SongRepository>();
            return services;
        }
    }
}
