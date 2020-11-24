using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using NewsPortal.Dal.Dtos;
using NewsPortal.Dal.Entities;
using System.Text;

namespace NewsPortal.Dal.Services
{
    public class CommentService
    {
        public CommentService(NewsPortalDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public NewsPortalDbContext DbContext { get; }

        public static Expression<Func<Comment, CommentDto>> CommentDtoSelector { get; } = c => new CommentDto
        {
            Id = c.Id,
            NewsId = c.NewsId,
            CreationDate = c.CreationDate,
            Text = c.Text,
            User = c.User.Name,
            UserId = c.UserId
        };

        public IEnumerable<CommentDto> GetComments(int id)
        {
            return DbContext.Comments
                .Where(c => c.NewsId == id)
                .OrderByDescending(c => c.Id)
                .Select(CommentDtoSelector)
                .AsEnumerable();
        }

        public CommentDto PostComment(int newsId, string text, int currentUserId)
        {
            var comment = DbContext.Comments.Add(new Comment
            {
                NewsId = newsId,
                CreationDate = DateTimeOffset.Now,
                Text = text,
                UserId = currentUserId
            });
            DbContext.SaveChanges();

            return DbContext.Comments
            .Where(c => c.Id == comment.Entity.Id)
            .Select(CommentDtoSelector)
            .Single();
        }
        public CommentDto DeleteComment(int commentId, int currentUserId)
        {
            var comment = DbContext.Comments
            .Where(c => c.Id == commentId && currentUserId == c.UserId)
            .Select(CommentDtoSelector)
            .Single();
            DbContext.Remove(new Comment { Id = commentId });
            DbContext.SaveChanges();
            return comment;
        }
    }
}
