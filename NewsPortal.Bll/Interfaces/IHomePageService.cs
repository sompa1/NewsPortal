using System.Threading.Tasks;

namespace NewsPortal.Bll.Interfaces {
    public interface IHomePageService
    {
        Task<string> GetHomePageContent();
        Task UpdateHomePage(string content);
    }
}
