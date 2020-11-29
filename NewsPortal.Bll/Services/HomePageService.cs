using Microsoft.EntityFrameworkCore;
using NewsPortal.Bll.Dtos;
using NewsPortal.Bll.Interfaces;
using NewsPortal.Dal;
using NewsPortal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Bll.Services
{
    public class HomePageService : IHomePageService
    {

        private readonly NewsPortalDbContext _dbContext;

        public HomePageService(NewsPortalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task UpdateHomePage(string content)
        {
            var homePageContent = new HomePageContent()
            {
                Id = 1,
                Content = content
            };
            _dbContext.HomePageContent.Update(homePageContent);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<string> GetHomePageContent()
        {
            HomePageContent hp = await _dbContext.HomePageContent.SingleAsync(h => h.Id == 1);
            return hp.Content;
        }
    }
}
