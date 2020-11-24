using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Dal.Entities;
using NewsPortal.Dal.Services;
using NewsPortal.Dal.Specifications;
using NewsPortal.Web.Models;
using System.Linq;

namespace NewsPortal.Web.Controllers {

    public class NewsController : Controller {

        public NewsService NewsService { get; }

        public CommentService CommentService { get; }

        public UserManager<User> UserManager { get; }

        public NewsController(NewsService newsService, CommentService commentService, UserManager<User> userManager) {
            NewsService = newsService;
            CommentService = commentService;
            UserManager = userManager;
        }

        private int? currentUserId;

        public int? CurrentUserId => User.Identity.IsAuthenticated ? (currentUserId ?? (currentUserId = int.Parse(UserManager.GetUserId(User)))) : null;

       [AllowAnonymous]
        public IActionResult Index(NewsSpecification specification)
        {
            if (specification?.PageNumber != null)
                specification.PageNumber -= 1;

            var news = NewsService.GetNews(specification);
            return View(news);
        }

        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index", "News");

            var news = NewsService.GetOneNews(id.Value);

            if (news == null)
                return NotFound();

            var comments = CommentService.GetComments(id.Value);

            var model = new NewsModel
            {
                News = news,
                Comments = comments,
                CurrentUserId = CurrentUserId
            };

            return View(model);
        }


        [HttpPost]
        public ActionResult AddComment(int newsId, string text)
        {
            CommentService.PostComment(newsId, text, CurrentUserId.Value);
            return RedirectToAction("Index", "News", new { id = newsId });
        }

    }
}
