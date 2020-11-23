using Microsoft.AspNetCore.Mvc;
using NewsPortal.Dal.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
