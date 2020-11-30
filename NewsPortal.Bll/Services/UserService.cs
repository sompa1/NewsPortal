using NewsPortal.Bll.Interfaces;
using NewsPortal.Dal;
using NewsPortal.Model;
using System.Threading.Tasks;

namespace NewsPortal.Bll.Services {

    public class UserService : IUserService {

        private readonly NewsPortalDbContext _dbContext;

        public UserService(NewsPortalDbContext dbContext) {
            _dbContext = dbContext;
        }

        public Task<int> RemoveUser(int userId) {
            _dbContext.Users.Remove(_dbContext.Users.Find(userId));
            return _dbContext.SaveChangesAsync();
        }
    }
}
