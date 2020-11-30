using System.Threading.Tasks;
using NewsPortal.Dal.SeedInterfaces;
using NewsPortal.Model.Users;
using Microsoft.AspNetCore.Identity;

namespace NewsPortal.Dal.SeedServices {
    public class RoleSeedService: IRoleSeedService
    {
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public RoleSeedService(RoleManager<IdentityRole<int>> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task SeedRoleAsync()
        {
            if (!await _roleManager.RoleExistsAsync(Roles.Administrators))
                await _roleManager.CreateAsync(new IdentityRole<int> { Name = Roles.Administrators });
            if (!await _roleManager.RoleExistsAsync(Roles.Authors))
                await _roleManager.CreateAsync(new IdentityRole<int> { Name = Roles.Authors });
        }
    }
}
