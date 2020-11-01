using NewsPortal.Dal.Dtos;
using NewsPortal.Dal.Entities;
using NewsPortal.Dal.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewsPortal.Dal.Services {

    public class NewsService {

        public NewsService(NewsPortalDbContext dbContext) {
            DbContext = dbContext;
        }

        public NewsPortalDbContext DbContext { get; }

        public IEnumerable<NewsDto> GetNews(NewsSpecification specification = null) {
            IQueryable<News> query = DbContext.News;

            return query.ToList().Select(n => new NewsDto {
                Id = n.Id,
                Body = n.Body
            });
        }
    }
}
