﻿using NewsPortal.Bll.Dtos;
using NewsPortal.Dal.Specifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsPortal.Bll.Interfaces
{
    public interface INewsService
    {
        Task<NewsDto> AddNews(int authorId, string headline, string shortDescription, string body);
        IEnumerable<NewsDto> GetAllNews(NewsSpecification specification = null);
        PagedResult<NewsDto> GetNews(NewsSpecification specification = null);
        Task<NewsDto> GetOneNews(int id);
    }
}
