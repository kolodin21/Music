using Microsoft.EntityFrameworkCore;
using Music.Application.Entity.Songs;
using Music.Application.HelperModels;
using Music.Application.ModelsDto.Song;
using Music.Repository.EfCore.Database;
using Music.Repository.EfCore.Extensions;

namespace Music.Repository.EfCore.Repositories;

public class SongRepository : ISongRepository
{
    private readonly IMusicDbContext _dbContext;
    public SongRepository(IMusicDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //public async Task<QueryResult<SongReadDto>> GetByIdAsync(int id)
    //{
    //    var album = await _dbContext.Albums
    //        .AsNoTracking()
    //        .Include(album => album.Songs)
    //        .FirstAsync(x => x.Id == id);
    //    return album;
    //}

    public async Task<QueryResult<SongReadDto>> GetByIdAsync(int id)
    {
        try
        {
            var resultSong = await _dbContext.Songs
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);

            return resultSong == null ?
                QueryResult<SongReadDto>.Failure(new[] { "Данной песни не существует" })
                : QueryResult<SongReadDto>.Success(SongDtoFactory.CreateRead(resultSong));
        }
        catch (Exception exp)
        {
            return QueryResult<SongReadDto>.Failure(new[] { exp.Message });
        }
    }

    public async Task<QueryResult<PagedResult<SongReadDto>>> GetAllAsync(int pageNumber, int pageSize)
    {
        try
        {
            var songsQuery = await _dbContext.Songs
                .Include(x => x.Artist)
                .AsNoTracking()
                .ToPagedResultAsync(pageNumber, pageSize);

            var songs = songsQuery.Select(SongDtoFactory.CreateRead);

            return QueryResult<PagedResult<SongReadDto>>.Success(songs);
        }
        catch (Exception exp)
        {
            return QueryResult<PagedResult<SongReadDto>>.Failure(new[] { exp.Message });
        }
    }

}