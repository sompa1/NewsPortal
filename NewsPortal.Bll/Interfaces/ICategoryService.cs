using Microsoft.AspNetCore.Mvc.Rendering;
using NewsPortal.Bll.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Bll.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetCategories();

        public IEnumerable<SelectListItem> GetAllCategory();
    }
}
