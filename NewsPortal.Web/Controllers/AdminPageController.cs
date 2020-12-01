using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Bll.Interfaces;
using NewsPortal.Model;
using NewsPortal.Web.Models;
using System.Linq;
using System.Threading.Tasks;

namespace NewsPortal.Web.Controllers
{

    public class AdminPageController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly INewsService _newsService;
        private readonly ICategoryService _categoryService;

        public AdminPageController(UserManager<User> userManager, IUserService userService, INewsService newsService, ICategoryService categoryService)
        {
            _userManager = userManager;
            _userService = userService;
            _newsService = newsService;
            _categoryService = categoryService;
        }

        private int? currentUserId;

        public int? CurrentUserId => User.Identity.IsAuthenticated ? (currentUserId ?? (currentUserId = int.Parse(_userManager.GetUserId(User)))) : null;

        [Authorize(Roles = "Administrators")]
        public IActionResult Index() {
            var model = new AdminPageModel {
                CurrentUserId = CurrentUserId ?? 0,
                Users = _userManager.Users.ToList()
            };
            return View(model);
        }

        [Authorize(Roles = "Administrators")]
        public IActionResult PopulateNews() {
            _categoryService.PopulateDbWithCategories().Wait();
            _newsService.PopulateDbWithNews().Wait();
            return RedirectToAction("Index", "AdminPage");
        }

        [Authorize(Roles = "Administrators")]
        public async Task<IActionResult> GrantAuthorRole(int id)
        {
            await _userManager.AddToRoleAsync(_userManager.Users.Where(u => u.Id == id).Single(), "Authors");
            return RedirectToAction("Index", "AdminPage");
        }

        [Authorize(Roles = "Administrators")]
        public async Task<IActionResult> RevokeAuthorRole(int id)
        {
            await _userManager.RemoveFromRoleAsync(_userManager.Users.Where(u => u.Id == id).Single(), "Authors");
            return RedirectToAction("Index", "AdminPage");
        }

        [Authorize(Roles = "Administrators")]
        public async Task<IActionResult> GrantAdminRole(int id)
        {
            await _userManager.AddToRoleAsync(_userManager.Users.Where(u => u.Id == id).Single(), "Administrators");
            return RedirectToAction("Index", "AdminPage");
        }

        [Authorize(Roles = "Administrators")]
        public async Task<IActionResult> RevokeAdminRole(int id)
        {
            if (id != CurrentUserId) {
                await _userManager.RemoveFromRoleAsync(_userManager.Users.Where(u => u.Id == id).Single(), "Administrators");
            }
            return RedirectToAction("Index", "AdminPage");
        }

        [Authorize(Roles = "Administrators")]
        public async Task<IActionResult> RemoveUser(int id)
        {
            if (id != CurrentUserId) {
                await _userService.RemoveUser(id);
            }
            return RedirectToAction("Index", "AdminPage");
        }
    }
}
