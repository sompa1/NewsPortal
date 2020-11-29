using Microsoft.AspNetCore.Mvc;
using NewsPortal.Bll.Interfaces;
using NewsPortal.Bll.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsPortal.Web.Views.Shared.Components
{
    public class CategoryListViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public CategoryListViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _categoryService.GetCategories());
        }
    }
}
