using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using NewsPortal.Bll.Dtos;
using NewsPortal.Bll.Interfaces;
using NewsPortal.Model;
using NewsPortal.Dal.Services;
using NewsPortal.Web.ViewRender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsPortal.Web.Hubs
{
    public class NewsHub: Hub<IHubClient>
    {
        private int? currentUserId;
        private readonly ICommentService _commentService;
        private readonly UserManager<User> _userManager;
        private readonly IViewRender _viewRender;

        public NewsHub(ICommentService commentService, UserManager<User> userManager, IViewRender viewRender)
        {
            _commentService = commentService;
            _userManager = userManager;
            _viewRender = viewRender;
        }

        public int? CurrentUserId => Context.User.Identity.IsAuthenticated
        ? (currentUserId ?? (currentUserId = int.Parse(_userManager.GetUserId(Context.User))))
        : null;
        public async Task JoinNewsPage(int newsId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"news-{newsId}");
        }
        public async Task PostComment(int newsId, string text)
        {
            var comment = await _commentService.PostComment(newsId, text, CurrentUserId.Value);
            var htmlString = _viewRender.Render<CommentDto>("Shared/_CommentPartial", comment);
            await Clients.Group($"news-{newsId}").CommentPosted(htmlString);
        }
        public async Task DeleteComment(int commentId)
        {
            var newsId = await _commentService.DeleteComment(commentId, CurrentUserId.Value);
            await Clients.Group($"news-{newsId}").CommentDeleted(commentId);
        }
    }

    public interface IHubClient
    {
        Task CommentDeleted(int commentId);
        Task CommentPosted(string htmlString);
    }

}
