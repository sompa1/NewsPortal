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

    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly INewsService _newsService;
        private readonly ICategoryService _categoryService;

        public UserController(UserManager<User> userManager, INewsService newsService, ICategoryService categoryService)
        {
            _userManager = userManager;
            _newsService = newsService;
            _categoryService = categoryService;
        }

        private int? currentUserId;

        public int? CurrentUserId => User.Identity.IsAuthenticated ? (currentUserId ?? (currentUserId = int.Parse(_userManager.GetUserId(User)))) : null;

        [Authorize(Roles = "Administrators")]
        public IActionResult List() {
            var model = new UserListModel {
                CurrentUserId = CurrentUserId ?? 0,
                Users = _userManager.Users
            };
            return View(model);
        }

        [Authorize(Roles = "Administrators")]
        public IActionResult PopulateNews() {
            _categoryService.PopulateDbWithCategories().Wait();
            _newsService.PopulateDbWithNews().Wait();
            return RedirectToAction("List", "User");
        }

        [Authorize(Roles = "Administrators")]
        public async Task<IActionResult> GrantAuthorRole(int id)
        {
            await _userManager.AddToRoleAsync(_userManager.Users.Where(u => u.Id == id).Single(), "Authors");
            return RedirectToAction("List", "User");
        }

        [Authorize(Roles = "Administrators")]
        public async Task<IActionResult> RevokeAuthorRole(int id)
        {
            await _userManager.RemoveFromRoleAsync(_userManager.Users.Where(u => u.Id == id).Single(), "Authors");
            return RedirectToAction("List", "User");
        }

        [Authorize(Roles = "Administrators")]
        public async Task<IActionResult> GrantAdminRole(int id)
        {
            await _userManager.AddToRoleAsync(_userManager.Users.Where(u => u.Id == id).Single(), "Administrators");
            return RedirectToAction("List", "User");
        }

        [Authorize(Roles = "Administrators")]
        public async Task<IActionResult> RevokeAdminRole(int id)
        {
            if (id != CurrentUserId) {
                await _userManager.RemoveFromRoleAsync(_userManager.Users.Where(u => u.Id == id).Single(), "Administrators");
            }
            return RedirectToAction("List", "User");
        }
    }
}
