using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Bll.Dtos;
using NewsPortal.Bll.Interfaces;
using NewsPortal.Model;
using NewsPortal.Dal.Specifications;
using NewsPortal.Web.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace NewsPortal.Web.Controllers
{

    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        private int? currentUserId;

        public int? CurrentUserId => User.Identity.IsAuthenticated ? (currentUserId ?? (currentUserId = int.Parse(_userManager.GetUserId(User)))) : null;

        [Authorize(Roles = "Administrators")]
        public async Task<IActionResult> List() {
            var model = new UserListModel {
                CurrentUserId = CurrentUserId ?? 0,
                Users = _userManager.Users
            };
            return View(model);
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
