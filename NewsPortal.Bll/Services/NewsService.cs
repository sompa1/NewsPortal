using Microsoft.EntityFrameworkCore;
using NewsPortal.Bll.Interfaces;
using NewsPortal.Bll.Dtos;
using NewsPortal.Model;
using NewsPortal.Dal.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NewsPortal.Dal;

namespace NewsPortal.Bll.Services {

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


        public PagedResult<NewsDto> GetNews(NewsSpecification specification)
        {
            if (specification.PageSize < 1)
                specification.PageSize = 1;
            if (specification.PageNumber < 1)
                specification.PageNumber = 1;

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

            switch (specification?.Order) {
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

            var allResultsCount = query.Count();
            query = query.Where(n => n.ExpirationDate >= today)
                .Skip((specification.PageNumber - 1) * specification.PageSize)
                .Take(specification.PageSize);
            return new PagedResult<NewsDto> {
                AllResultsCount = allResultsCount,
                Results = query.ToList().Select(NewsDtoSelectorFunc.Value),
                PageNumber = specification.PageNumber,
                PageSize = specification.PageSize
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

        public Task<int> PopulateDbWithNews() {
            _dbContext.AddRange(new News {
                    Headline = @"There Were 2 Mass Shootings In Texas Last Week, But Only 1 On TV",
                    AuthorId = 2,
                    ShortDescription = @"She left her husband. He killed their children. Just another day in America.",
                    Body = @"<div><p><span>On the evening of May 15, Amanda and Justin took the kids out for pizza. During the car ride, Amanda told Justin about her new boyfriend, that it was serious and he was planning to move in with her.  </span></p></div><div><p><span> For the past few months, Amanda had been in a long-distance relationship with Seth Richardson, whom she knew from her teen years.They, too, had met through “World of Warcraft,” and had kept in touch for a decade. </span></p></div><div><p><span>“We just had this bond, it never left,” Amanda said. “There was so much positivity when we were together.” </span></p></div>",
                    CategoryId = _dbContext.Categories.Where(c => c.Name.Equals("Crime")).Select(c => c.Id).Single(),
                    PublishDate = new DateTime(2018, 5, 26, 7, 0, 0),
                    ExpirationDate = new DateTime(2021, 5, 26, 7, 0, 0)
                },
                new News {
                    Headline = @"Will Smith Joins Diplo And Nicky Jam For The 2018 World Cup's Official Song",
                    AuthorId = 2,
                    ShortDescription = @"Of course it has a song.",
                    Body = @"<div><p>The <a href=""https://www.huffpost.com/topic/fifa-world-cup"">2018 FIFA World Cup</a> starts June 14 in Russia, and now it has an official song. </p></div><div><p>Producer Diplo and reggaeton star Nicky Jam collaborate on “Live It Up,” which also features Albanian singer Era Istrefi and actor Will Smith, who is trying to<a href=""https://youtu.be/6wGj89GE6aQ""> restart his music career</a>.</p></div><div><p>Traditionally, over the last two decades, each World Cup has had an official song.The song for the 2010 World Cup in South Africa was<a href=""https://www.youtube.com/watch?v=pRpeEdMmmQ0"">“Waka Waka” by Shakira</a>. For the 2014 World Cup in Brazil, the song was<a href=""https://www.youtube.com/watch?v=9W3sWiZ-iO8"">“We Are One (Ole Ola)” by Pitbull</a>.</p></div>",
                    CategoryId = _dbContext.Categories.Where(c => c.Name.Equals("Entertainment")).Select(c => c.Id).Single(),
                    PublishDate = new DateTime(2018, 5, 26, 8, 0, 0),
                    ExpirationDate = new DateTime(2021, 5, 26, 7, 0, 0)
                },
                new News {
                    Headline = @"With Its Way Of Life At Risk, This Remote Oyster-Growing Region Called In Robots",
                    AuthorId = 2,
                    ShortDescription = @"The revolution is coming to rural New Brunswick.",
                    Body = @"<div><p><span>NEGUAC, Canada ― When the harbors aren’t frozen, Maxime Daigle and his older brother Jean-Francois often take to the cold waters off New Brunswick in one of their family’s flat-bottomed boats to harvest oysters. The siblings are among hundreds of producers along this rocky coast who grow oysters for La Maison BeauSoleil in the company’s distinctive floating bags. </span></p></div>< div><p>For the Daigles this is a family business.Maxime’s father, Maurice, co-owns La Maison BeauSoleil, an oyster grower, packer and distributor that he runs with his business partner, Amédée Savoie, whose son, Allain Savoie, also works in the company.</p></div><div><p><span> When the business started growing and selling oysters from the coastal village of Neguac, at the southern end of the Acadian Peninsula, each wooden box was painstakingly packed by hand. </span></p></div><div><p><span> That was 18 years ago.La Maison BeauSoleil ― </span>called simply BeauSoleil within the seafood business ― <span> has since become Canada’s largest producer of cocktail-sized oysters. Demand for its tasty, teardrop-shaped product is at an all-time high. While the company is working hard to keep up, business as usual isn’t cutting it.</span> </p></div><div><p><span> So a new generation of robots is coming.</span></p></div><div><p><span> While details are still closely guarded, the company is developing a state-of-the-art automated sorting and packing line that will revolutionize its business.Software-controlled robots,</span> <span> able to mimic the small, complicated movements of human oyster pickers</span><strong>, </strong><span> will give the company a new competitive edge, says Savoie, and allow them to double production. </span></p></div><div><p><span> This is the type of investment and technology that has often been missing from this country’s traditional fishing economy.Many of Atlantic Canada’s rural coastal communities have been treading water since<a href=""http://www.cbc.ca/news/canada/remembering-the-mighty-cod-fishery-20-years-after-moratorium-1.1214172""> overfishing caused cod to collapse</a> in the 1990s, and falling further behind could signal the end for the region’s historic way of life. </span></p></div><div><p><span> But this is a family business, and the co-owners are trying to build a legacy for their children in this remote region.</span></p></div>",
                    CategoryId = _dbContext.Categories.Where(c => c.Name.Equals("Impact")).Select(c => c.Id).Single(),
                    PublishDate = new DateTime(2018, 5, 27, 8, 0, 0),
                    ExpirationDate = new DateTime(2021, 5, 26, 7, 0, 0)
                });
            return _dbContext.SaveChangesAsync();
        }
    }
}
