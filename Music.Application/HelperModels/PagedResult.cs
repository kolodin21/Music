namespace Music.Application.HelperModels
{
    /// <summary>
    /// Результат с пагинацией
    /// </summary>
    /// <typeparam name="T">Тип данных</typeparam>
    public class PagedResult<T>
    {
        public IReadOnlyCollection<T> Items { get; }
        public int TotalCount { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

        public PagedResult(
            IEnumerable<T> items,
            int totalCount,
            int pageNumber,
            int pageSize)
        {
            Items = items.ToList().AsReadOnly();
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
        public PagedResult<TResult> Select<TResult>(Func<T, TResult> selector)
        {
            return new PagedResult<TResult>(
                Items.Select(selector),
                TotalCount,
                PageNumber,
                PageSize
            );
        }
    }
}
