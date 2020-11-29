using Microsoft.AspNetCore.Mvc;
using NewsPortal.Bll.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsPortal.Web.Views.Shared.Components
{
    public class NewsDetailsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(NewsDto newsDto)
        {
            return View(newsDto);
        }
    }
}
