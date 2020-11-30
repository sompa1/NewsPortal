using Microsoft.AspNetCore.Mvc;
using NewsPortal.Bll.Dtos;

namespace NewsPortal.Web.Views.Shared.Components {
    public class NewsDetailsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(NewsDto newsDto)
        {
            return View(newsDto);
        }
    }
}
