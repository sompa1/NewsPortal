﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Dal.Entities;
using NewsPortal.Dal.Services;
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
       public IActionResult Index(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index", "Home");

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

        /*
        public IActionResult News() {

            var news = NewsService.GetNews().Select(n => new NewsModel {
                Body = n.Body
            });

            return View(news);
        }
        */
    }
}
