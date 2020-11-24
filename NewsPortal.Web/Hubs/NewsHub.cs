using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using NewsPortal.Dal.Dtos;
using NewsPortal.Dal.Entities;
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
        public CommentService CommentService { get; }
        public UserManager<User> UserManager { get; }
        public IViewRender ViewRender { get; }
        public NewsHub(CommentService commentService, UserManager<User> userManager, IViewRender viewRender)
        {
            CommentService = commentService;
            UserManager = userManager;
            ViewRender = viewRender;
        }
        private int? currentUserId;
        public int? CurrentUserId => Context.User.Identity.IsAuthenticated
        ? (currentUserId ?? (currentUserId = int.Parse(UserManager.GetUserId(Context.User))))
        : null;
        public async Task JoinNewsPage(int newsId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"news-{newsId}");
        }
        public async Task PostComment(int newsId, string text)
        {
            var comment = CommentService.PostComment(newsId, text, CurrentUserId.Value);
            var htmlString = ViewRender.Render<CommentDto>("Shared/_CommentPartial", comment);
            await Clients.Group($"news-{newsId}").CommentPosted(htmlString);
        }
        public async Task DeleteComment(int commentId)
        {
            var comment = CommentService.DeleteComment(commentId, CurrentUserId.Value);
            await Clients.Group($"news-{comment.NewsId}").CommentDeleted(commentId);
        }
    }

    public interface IHubClient
    {
        Task CommentDeleted(int commentId);
        Task CommentPosted(string htmlString);
    }

}
