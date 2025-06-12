using Microsoft.EntityFrameworkCore;
using Music.Application.Extensions;

namespace Music.Infrastructure.SQLite.Extensions
{
    public static class QueryableExtensions
    {

        /// <summary>
        /// Применяет пагинацию к IQueryable
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <param name="query">Запрос</param>
        /// <param name="pageNumber">Номер страницы (начинается с 1)</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <returns>Запрос с примененной пагинацией</returns>
        public static IQueryable<T> WithPagination<T>(
            this IQueryable<T> query,
            int pageNumber,
            int pageSize)
        {
            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Номер страницы должен быть больше 0");

            if (pageSize < 1)
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Размер страницы должен быть больше 0");

            return query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
        }

        /// <summary>
        /// Применяет пагинацию и возвращает результат с метаданными
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <param name="query">Запрос</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <returns>Результат с данными и метаданными пагинации</returns>
        public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
            this IQueryable<T> query,
            int pageNumber,
            int pageSize)
        {
            var totalCount = await query.CountAsync();
            var items = await query
                .WithPagination(pageNumber, pageSize)
                .ToListAsync();

            return new PagedResult<T>(
                items,
                totalCount,
                pageNumber,
                pageSize);
        }
    }
}
