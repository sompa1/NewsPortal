using NewsPortal.Bll.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsPortal.Bll.Interfaces {
    public interface ICommentService
    {
        Task<int> DeleteComment(int commentId, int currentUserId);
        Task<CommentDto> GetComment(int id);
        IEnumerable<CommentDto> GetComments(int newsId);
        Task<CommentDto> PostComment(int newsId, string text, int currentUserId);
    }
}
