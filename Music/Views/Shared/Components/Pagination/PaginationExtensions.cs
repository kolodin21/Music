using Music.Application.HelperModels;

namespace Music.Views.Shared.Components.Pagination;

public static class PaginationExtensions
{
    public static Pagination ToPagination<T>(
        this PagedResult<T> pagedList,
        string controllerName,
        string actionName)
    {
        return new Pagination
        {
            ControllerName = controllerName,
            ActionName = actionName,
            PageNumber = pagedList.PageNumber,
            PageSize = pagedList.PageSize,
            TotalCount = pagedList.TotalCount,
            TotalPages = pagedList.TotalPages
        };
    }
}