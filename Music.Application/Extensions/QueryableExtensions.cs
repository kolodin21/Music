using System.Linq.Expressions;

namespace Music.Application.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyIf<T>(this IQueryable<T> query, bool condition,
            Expression<Func<T, bool>> predicate)
        {
            return condition ? query.Where(predicate) : query;
        }
    }
}
