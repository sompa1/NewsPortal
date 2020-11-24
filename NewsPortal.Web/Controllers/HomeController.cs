using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsPortal.Dal.Services;
using NewsPortal.Dal.Specifications;
using NewsPortal.Web.Models;

namespace NewsPortal.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NewsService _newsService;


        public HomeController(ILogger<HomeController> logger, NewsService newsService)
        {
            _logger = logger;
            _newsService = newsService;
        }

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
