using Microsoft.AspNetCore.Mvc;
using NewsPortal.Dal.Services;
using NewsPortal.Web.Models;
using System.Linq;

namespace NewsPortal.Web.Controllers {

    public class NewsController : Controller {

        public NewsService NewsService { get; }

        public NewsController(NewsService newsService) {
            NewsService = newsService;
        }

        public IActionResult News() {

            var news = NewsService.GetNews().Select(n => new NewsModel {
                Body = n.Body
            });

            return View(news);
        }
    }
}
