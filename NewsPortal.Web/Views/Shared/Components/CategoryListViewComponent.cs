using Microsoft.AspNetCore.Mvc;
using NewsPortal.Bll.Interfaces;
using System.Threading.Tasks;

namespace NewsPortal.Web.Views.Shared.Components {
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
