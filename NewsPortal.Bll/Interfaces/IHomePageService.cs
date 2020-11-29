using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Bll.Interfaces
{
    public interface IHomePageService
    {
        Task<string> GetHomePageContent();
        Task UpdateHomePage(string content);
    }
}
