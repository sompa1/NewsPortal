using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Bll.Interfaces;
using NewsPortal.Dal.Services;
using NewsPortal.Dal.Specifications;
using NewsPortal.Web.Models;

namespace NewsPortal.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomePageService _homePageService;

        public HomeController(IHomePageService homePageService)
        {
            _homePageService = homePageService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var content = await _homePageService.GetHomePageContent();
            return View(new HomeViewModel() { Content = content });
        }

        [HttpGet]
        [Authorize(Roles = "Authors")]
        public async Task<IActionResult> Edit()
        {
            var content = await _homePageService.GetHomePageContent();
            return View(new HomeViewModel() { Content = content });
        }

        [HttpPost]
        [Authorize(Roles = "Authors")]
        public async Task<IActionResult> Edit(HomeViewModel model)
        {
            await _homePageService.UpdateHomePage(model.Content);
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
