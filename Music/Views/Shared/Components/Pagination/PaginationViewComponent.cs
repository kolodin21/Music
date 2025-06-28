using Microsoft.AspNetCore.Mvc;

namespace Music.Views.Shared.Components.Pagination
{
    public class PaginationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Pagination pagination)
        {
            return View(pagination);
        }
    }
}
