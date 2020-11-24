using Microsoft.AspNetCore.Mvc;
using NewsPortal.Dal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsPortal.Web.Views.Shared.Components
{
    public class CategoryListViewComponent : ViewComponent
    {
        public CategoryListViewComponent(CategoryService categoryService)
        {
            CategoryService = categoryService;
        }
        public CategoryService CategoryService { get; }
        public IViewComponentResult Invoke()
        {
            return View(CategoryService.GetCategories());
        }
    }
}
