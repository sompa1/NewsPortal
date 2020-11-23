using Microsoft.EntityFrameworkCore;
using NewsPortal.Dal.Dtos;
using NewsPortal.Dal.Entities;
using NewsPortal.Dal.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NewsPortal.Dal.Services {

    public class NewsService {

        public static Expression<Func<News, NewsDto>> NewsDtoSelector { get; } = n => new NewsDto
        {
            Author = n.Author.Name,
            AuthorId = n.AuthorId,
            CategoryId = n.CategoryId,
            Id = n.Id,
            NumberOfComments = n.Comments.Count(),
            PublishYear = n.PublishDate.Year,
            ShortDescription = n.ShortDescription,
            Headline = n.Headline,
            Body = n.Body
        };
        public NewsService(NewsPortalDbContext dbContext) {
            DbContext = dbContext;
        }

        public NewsPortalDbContext DbContext { get; }

        public IEnumerable<NewsDto> GetNews(NewsSpecification specification = null) {
            IQueryable<News> query = DbContext
                .News.Include(n => n.Author)
                .Include(n => n.Comments);


            return query.ToList().Select(n => new NewsDto {
                Id = n.Id,
                Author = n.Author.Name,
                AuthorId = n.AuthorId,
                CategoryId = n.CategoryId,
                Body = n.Body,
                NumberOfComments = n.Comments.Count(),
                ShortDescription = n.ShortDescription,
                Headline = n.Headline,
                PublishYear = n.PublishDate.Year
            });
        }

        public IEnumerable<NewsDto> GetAllNews(NewsSpecification specification = null)
        {
            IQueryable<News> query = DbContext
                .News.Include(n => n.Author)
                .Include(n => n.Comments);


            return query.Select(n => new NewsDto
            {
                Id = n.Id,
                Author = n.Author.Name,
                AuthorId = n.AuthorId,
                CategoryId = n.CategoryId,
                Body = n.Body,
                NumberOfComments = n.Comments.Count(),
                ShortDescription = n.ShortDescription,
                Headline = n.Headline,
                PublishYear = n.PublishDate.Year
            }).ToList();
        }

        public NewsDto GetOneNews(int id)
        {
            return DbContext.News.Where(n => n.Id == id).Select(NewsDtoSelector).SingleOrDefault();
        }
    }
}
