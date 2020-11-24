using Microsoft.EntityFrameworkCore;
using NewsPortal.Dal.Dtos;
using NewsPortal.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewsPortal.Dal.Services
{
    public class CategoryService
    {
        private NewsPortalDbContext DbContext { get; }
        public CategoryService(NewsPortalDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public IEnumerable<CategoryDto> GetCategories()
        {
            //var allCategories = DbContext.Categories.ToList();

            IQueryable<Category> query = DbContext
                .Categories.Include(c => c.News);


            return query.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }
    }
}
