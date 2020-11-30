using Microsoft.AspNetCore.Mvc;
using NewsPortal.Bll.Dtos;
namespace NewsPortal.Web.Views.Shared.Components
{
    public class NewsListViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke(PagedResult<NewsDto> news)
        {
            return View(news);
        }
    }
}
