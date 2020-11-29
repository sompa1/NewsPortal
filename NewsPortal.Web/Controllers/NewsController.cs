using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Bll.Dtos;
using NewsPortal.Bll.Interfaces;
using NewsPortal.Model;
using NewsPortal.Dal.Specifications;
using NewsPortal.Web.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace NewsPortal.Web.Controllers
{

    public class NewsController : Controller
    {

        private readonly INewsService _newsService;
        private readonly ICommentService _commentService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<User> _userManager;

        public NewsController(INewsService newsService, ICommentService commentService, ICategoryService categoryService, UserManager<User> userManager)
        {
            _userManager = userManager;
            _newsService = newsService;
            _commentService = commentService;
            _categoryService = categoryService;
        }

        private int? currentUserId;


        public int? CurrentUserId => User.Identity.IsAuthenticated ? (currentUserId ?? (currentUserId = int.Parse(_userManager.GetUserId(User)))) : null;

        [AllowAnonymous]
        public IActionResult Index(NewsIndexModel model)
        {
            var list = _categoryService.GetAllCategory();
            model.Categories = list;
            if (model.Specification?.PageNumber != null)
                model.Specification.PageNumber -= 1;

            model.News = _newsService.GetNews(model.Specification);
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index", "News");

            var news = await _newsService.GetOneNews(id.Value);

            if (news == null)
                return NotFound();

            var comments = _commentService.GetComments(id.Value);

            var model = new NewsModel
            {
                News = news,
                Comments = comments,
                CurrentUserId = CurrentUserId
            };

            return View(model);
        }

        [Authorize(Roles = "Authors")]
        public IActionResult Write()
        {
            var list = _categoryService.GetAllCategory();
            return View(new CreateNewsModel() { Categories = list });
        }

        [Authorize(Roles = "Authors")]
        [HttpPost]
        public async Task<ActionResult> AddNews(CreateNewsModel model)
        {
            var list = _categoryService.GetAllCategory();
            var news = await _newsService.AddNews(CurrentUserId.Value, model.Headline, model.ShortDescription, model.Body, model.CategoryId, model.ExpirationDate);
            return RedirectToAction("Details", "News", new { id = news.Id });
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddComment(int newsId, string text)
        {
            await _commentService.PostComment(newsId, text, CurrentUserId.Value);
            return RedirectToAction("Details", "News", new { id = newsId });
        }

        [Authorize]
        public async Task<ActionResult> DeleteComment(int id)
        {
            var newsId = await _commentService.DeleteComment(id, CurrentUserId.Value);
            return RedirectToAction("Details", "News", new { id = newsId });
        }

    }
}
