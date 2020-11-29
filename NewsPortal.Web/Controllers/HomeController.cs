using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Bll.Interfaces;
using NewsPortal.Dal.Services;
using NewsPortal.Dal.Specifications;
using NewsPortal.Web.Models;

namespace NewsPortal.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly INewsService _newsService;


        public HomeController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [AllowAnonymous]
        public IActionResult Index( NewsSpecification specification)
        {
            if (specification?.PageNumber != null)
                specification.PageNumber -= 1;

            var news = _newsService.GetNews(specification);
            return View(news);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
