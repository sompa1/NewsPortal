using Microsoft.AspNetCore.Mvc.Rendering;
using NewsPortal.Bll.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsPortal.Bll.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetCategories();
        IEnumerable<SelectListItem> GetAllCategory();
        Task<CategoryDto> GetCategoryById(int id);
        Task UpdateCategoryById(CategoryDto dto);
        Task CreateCategory(CategoryDto dto);
        Task DeleteCategoryById(CategoryDto dto);
    }
}
