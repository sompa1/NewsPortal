using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using NewsPortal.Dal.Entities;
using NewsPortal.Dal.SeedInterfaces;
using NewsPortal.Dal.Users;
using Microsoft.AspNetCore.Identity;

namespace NewsPortal.Dal.SeedServices
{
    public class UserSeedService: IUserSeedService
    {

        private readonly UserManager<User> _userManager;

        public UserSeedService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task SeedUserAsync()
        {
            if (!(await _userManager.GetUsersInRoleAsync(Roles.Administrators)).Any())
            {
                var user = new User
                {
                    Email = "admin@newsportal.hu",
                    Name = "Adminisztrátor",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "admin"
                };

                var createResult = await _userManager.CreateAsync(user, "$Administrator123");
                var addToRoleResult = await _userManager.AddToRoleAsync(user, Roles.Administrators);

                if (!createResult.Succeeded || !addToRoleResult.Succeeded)
                    throw new ApplicationException($"Administrator could not be created: " +
                    $"{string.Join(", ", createResult.Errors.Concat(addToRoleResult.Errors).Select(e => e.Description))}");
            }
        }
    }
}
