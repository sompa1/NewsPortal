using System.Threading.Tasks;

namespace NewsPortal.Bll.Interfaces {
    public interface IUserService {
        Task<int> RemoveUser(int userId);
    }
}
