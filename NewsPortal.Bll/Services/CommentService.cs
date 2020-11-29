using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using NewsPortal.Bll.Dtos;
using NewsPortal.Model;
using System.Text;
using NewsPortal.Bll.Interfaces;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NewsPortal.Dal.Services
{
    public class CommentService : ICommentService
    {
        private readonly NewsPortalDbContext _dbContext;

        public CommentService(NewsPortalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public static Expression<Func<Comment, CommentDto>> CommentDtoSelector { get; } = c => new CommentDto
        {
            Id = c.Id,
            NewsId = c.NewsId,
            CreationDate = c.CreationDate,
            Text = c.Text,
            User = c.User.UserName,
            UserId = c.UserId
        };

        public IEnumerable<CommentDto> GetComments(int newsId)
        {
            return _dbContext.Comments.Include(c => c.User)
                .Where(c => c.NewsId == newsId)
                .OrderByDescending(c => c.Id)
                .Select(CommentDtoSelector);
        }

        public Task<CommentDto> GetComment(int id)
        {
            return _dbContext.Comments.Include(c => c.User)
                .Select(CommentDtoSelector).SingleAsync(c => c.Id == id);
        }

        public async Task<CommentDto> PostComment(int newsId, string text, int currentUserId)
        {
            var comment = _dbContext.Comments.Add(new Comment
            {
                NewsId = newsId,
                CreationDate = DateTimeOffset.Now,
                Text = text,
                UserId = currentUserId
            });
            await _dbContext.SaveChangesAsync();

            return await _dbContext.Comments
            .Select(CommentDtoSelector)
            .SingleAsync(c => c.Id == comment.Entity.Id);
        }

        public async Task<int> DeleteComment(int commentId, int currentUserId)
        {
            var comment = await _dbContext.Comments
            .Where(c => c.Id == commentId && currentUserId == c.UserId)
            .SingleAsync();
            _dbContext.Remove(comment);
            await _dbContext.SaveChangesAsync();
            return comment.NewsId;
        }
    }
}
