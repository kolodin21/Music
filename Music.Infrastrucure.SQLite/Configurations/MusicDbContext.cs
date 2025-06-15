using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;
using Music.Domain.Models;
using Music.Repository.EfCore.Database;

namespace Music.Infrastructure.SQLite.Configurations;

public class MusicDbContext : DbContext, IMusicDbContext
{
    public MusicDbContext(DbContextOptions<MusicDbContext> options)
        : base(options)
    {
    }
    public DbSet<Album> Albums { get; set; }
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Song> Songs { get; set; }


    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await Database.BeginTransactionAsync();
    }
}

public class MusicDbContextFactory : IDesignTimeDbContextFactory<MusicDbContext>
{
    public MusicDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MusicDbContext>();
        optionsBuilder.UseSqlite($"Data Source=MusicDb.db");

        return new MusicDbContext(optionsBuilder.Options);
    }
}