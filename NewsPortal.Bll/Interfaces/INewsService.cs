using NewsPortal.Bll.Dtos;
using NewsPortal.Dal.Specifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsPortal.Bll.Interfaces
{
    public interface INewsService
    {
        Task<NewsDto> AddNews(int authorId, string headline, string shortDescription, string body, int categoryId, DateTime expirationDate);
        IEnumerable<NewsDto> GetAllNews(NewsSpecification specification = null);
        PagedResult<NewsDto> GetNews(NewsSpecification specification);
        Task<NewsDto> GetOneNews(int id);
        Task<int> PopulateDbWithNews();
    }
}
