using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsPortal.Bll.Dtos;
using NewsPortal.Bll.Interfaces;
using NewsPortal.Dal;
using NewsPortal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Bll.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly NewsPortalDbContext _dbContext;

        public CategoryService(NewsPortalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<CategoryDto>> GetCategories()
        {
            //var allCategories = DbContext.Categories.ToList();

            IQueryable<Category> query = _dbContext
                .Categories.Include(c => c.News);


            return query.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            }).ToListAsync();
        }

        public IEnumerable<SelectListItem> GetAllCategory()
        {
            IEnumerable<SelectListItem> list = _dbContext.Categories.Select(c => new SelectListItem
            {
                Selected = false,
                Text = c.Name,
                Value = (c.Id).ToString()
            });

            return list;
        }
    }
}
