using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsPortal.Bll.Dtos;
using NewsPortal.Bll.Interfaces;
using NewsPortal.Dal;
using NewsPortal.Model;
using System.Collections.Generic;
using System.Linq;
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
            return _dbContext
                .Categories.Select(ent => new CategoryDto
                {
                    Id = ent.Id,
                    Name = ent.Name
                }).ToListAsync();
        }

        public Task<CategoryDto> GetCategoryById(int id)
        {
            return _dbContext
                .Categories.Select(ent => new CategoryDto
                {
                    Id = ent.Id,
                    Name = ent.Name
                }).SingleAsync(ent => ent.Id == id);
        }

        public async Task DeleteCategoryById(CategoryDto dto)
        {
            var category = new Category()
            {
                Id = dto.Id,
                Name = dto.Name
            };
            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCategoryById(CategoryDto dto)
        {
            var category = new Category()
            {
                Id = dto.Id,
                Name = dto.Name
            };
            _dbContext.Categories.Update(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateCategory(CategoryDto dto)
        {
            var category = new Category()
            {
                Name = dto.Name
            };
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<SelectListItem> GetAllCategory()
        {
            return _dbContext
                .Categories.Select(c => new SelectListItem
                {
                    Selected = false,
                    Text = c.Name,
                    Value = (c.Id).ToString()
                });
        }

        public Task<int> PopulateDbWithCategories() {
            _dbContext.AddRange(new Category() {
                    Name = "Crime"
                },
                new Category() {
                    Name = "Entertainment"
                },
                new Category() {
                    Name = "Politics"
                },
                new Category() {
                    Name = "World News"
                },
                new Category() {
                    Name = "Impact"
                },
                new Category() {
                    Name = "Weird News"
                },
                new Category() {
                    Name = "Black Voices"
                },
                new Category() {
                    Name = "Women"
                },
                new Category() {
                    Name = "Comedy"
                },
                new Category() {
                    Name = "Sports"
                },
                new Category() {
                    Name = "Business"
                },
                new Category() {
                    Name = "Tech"
                });
            return _dbContext.SaveChangesAsync();
        }
    }
}
