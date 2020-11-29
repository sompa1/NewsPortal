using Microsoft.EntityFrameworkCore;
using NewsPortal.Bll.Interfaces;
using NewsPortal.Bll.Dtos;
using NewsPortal.Model;
using NewsPortal.Dal.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NewsPortal.Dal;

namespace NewsPortal.Bll.Services
{

    public class NewsService : INewsService
    {
        private readonly NewsPortalDbContext _dbContext;

        public static Lazy<Func<News, NewsDto>> NewsDtoSelectorFunc { get; } = new Lazy<Func<News, NewsDto>>(() => NewsDtoSelector.Compile());
        public static Expression<Func<News, NewsDto>> NewsDtoSelector { get; } = n => new NewsDto
        {
            Author = n.Author.UserName,
            AuthorId = n.AuthorId,
            CategoryId = n.CategoryId,
            Category = n.Category.Name,
            Id = n.Id,
            NumberOfComments = n.Comments.Count(),
            PublishYear = n.PublishDate.Year,
            ShortDescription = n.ShortDescription,
            Headline = n.Headline,
            Body = n.Body
        };

        public NewsService(NewsPortalDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public PagedResult<NewsDto> GetNews(NewsSpecification specification = null)
        {

            if (specification?.PageSize < 0)
                specification.PageSize = null;
            if (specification?.PageNumber < 0)
                specification.PageNumber = null;

            IQueryable<News> query = _dbContext
                .News.Include(n => n.Author)
                .Include(n => n.Comments)
                .Include(n => n.Category);

            if (!string.IsNullOrWhiteSpace(specification?.Author))
                query = query.Where(n => n.Author.Name.Contains(specification.Author));
            if (specification?.CategoryId != null)
                query = query.Where(n => n.CategoryId == specification.CategoryId);
            if (!string.IsNullOrWhiteSpace(specification?.Headline))
                query = query.Where(n => n.Headline.Contains(specification.Headline));
            if (specification?.MinPublishYear != null)
                query = query.Where(n => n.PublishDate.Year >= specification.MinPublishYear);
            if (specification?.MaxPublishYear != null)
                query = query.Where(n => n.PublishDate.Year <= specification.MaxPublishYear);
            if (specification?.MinNumberOfComments != null)
                query = query.Where(n => n.Comments.Count() >= specification.MinNumberOfComments);
            if (specification?.MaxNumberOfComments != null)
                query = query.Where(n => n.Comments.Count() <= specification.MaxNumberOfComments);

            switch (specification?.Order)
            {
                case NewsSpecification.NewsOrder.AuthorAscending:
                    query = query.OrderBy(n => n.Author);
                    break;
                case NewsSpecification.NewsOrder.AuthorDescending:
                    query = query.OrderByDescending(n => n.Author);
                    break;
                case NewsSpecification.NewsOrder.PublishYearDescending:
                    query = query.OrderByDescending(n => n.PublishDate.Year);
                    break;
                case NewsSpecification.NewsOrder.PublishYearAscending:
                    query = query.OrderBy(n => n.PublishDate.Year);
                    break;
                case NewsSpecification.NewsOrder.NumberOfCommentsDescending:
                    query = query.OrderByDescending(n => n.Comments.Count());
                    break;
                case NewsSpecification.NewsOrder.NumberOfCommentsAscending:
                    query = query.OrderBy(n => n.Comments.Count());
                    break;
            }
            var today = DateTime.Today;

            int? allResultsCount = null;
            if ((specification?.PageSize ?? 0) != 0)
            {
                specification.PageNumber = specification.PageNumber ?? 0;
                allResultsCount = query.Count();
                query = query
                .Where(n => n.ExpirationDate > today)
                .Skip(specification.PageNumber.Value * specification.PageSize.Value)
                .Take(specification.PageSize.Value);
            }
            return new PagedResult<NewsDto>
            {
                AllResultsCount = allResultsCount,
                Results = query.ToList().Select(NewsDtoSelectorFunc.Value),
                PageNumber = specification?.PageNumber,
                PageSize = specification?.PageSize
            };
        }

        public IEnumerable<NewsDto> GetAllNews(NewsSpecification specification = null)
        {
            var today = DateTime.Today;

            IQueryable<News> query = _dbContext
                .News.Include(n => n.Author)
                .Include(n => n.Comments)
                .Include(n => n.Category);


            return query.Where(n=> n.ExpirationDate > today).Select(n => new NewsDto
            {
                Id = n.Id,
                Author = n.Author.Name,
                AuthorId = n.AuthorId,
                CategoryId = n.CategoryId,
                Category = n.Category.Name,
                Body = n.Body,
                NumberOfComments = n.Comments.Count(),
                ShortDescription = n.ShortDescription,
                Headline = n.Headline,
                PublishYear = n.PublishDate.Year
            }).ToList();
        }

        public async Task<NewsDto> AddNews(int authorId, string headline, string shortDescription, string body, int categoryId, DateTime expirationDate)
        {
            var news = _dbContext.News.Add(new News
            {
                AuthorId = authorId,
                Headline = headline,
                ShortDescription = shortDescription,
                Body = body,
                CategoryId = categoryId,
                PublishDate = DateTime.Now,
                ExpirationDate = expirationDate
            });
            await _dbContext.SaveChangesAsync();

            return await _dbContext.News.Select(NewsDtoSelector).SingleAsync(n => n.Id == news.Entity.Id);
        }

        public Task<NewsDto> GetOneNews(int id)
        {
            return _dbContext.News.Select(NewsDtoSelector).SingleAsync(n => n.Id == id);
        }
    }
}
