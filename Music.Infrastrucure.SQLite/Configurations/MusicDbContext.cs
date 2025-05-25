using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Music.Domain.Models;

namespace Music.Infrastructure.SQLite.Configurations;

public class MusicDbContext : DbContext
{
    public MusicDbContext(DbContextOptions<MusicDbContext> options)
        : base(options)
    {
    }
    public DbSet<Album> Albums { get; set; }
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Song> Songs { get; set; }
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