using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Bll.Dtos;
using NewsPortal.Bll.Interfaces;
using NewsPortal.Web.Models;

namespace NewsPortal.Web.Controllers {
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetCategories();
            return View(categories.Select(ent => new CategoryViewModel() { Id = ent.Id, Name = ent.Name }).ToList());
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            return View(new CategoryViewModel() { Id = category.Id, Name = category.Name });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(CategoryViewModel model)
        {
            var dto = new CategoryDto()
            {
                Id = model.Id,
                Name = model.Name
            };
            await _categoryService.UpdateCategoryById(dto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View(new CategoryViewModel());
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            return View(new CategoryViewModel() { Id = category.Id, Name = category.Name });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(CategoryViewModel model)
        {
            var dto = new CategoryDto()
            {
                Id = model.Id,
                Name = model.Name
            };
            await _categoryService.DeleteCategoryById(dto);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            var dto = new CategoryDto()
            {
                Name = model.Name
            };
            await _categoryService.CreateCategory(dto);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
