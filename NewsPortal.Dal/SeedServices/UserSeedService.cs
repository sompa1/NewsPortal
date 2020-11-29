using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using NewsPortal.Model;
using NewsPortal.Dal.SeedInterfaces;
using NewsPortal.Model.Users;
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
                    Email = "admin@newsportal.com",
                    Name = "Administrator",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "admin",
                };

                var author = new User
                {
                    Email = "author@newsportal.com",
                    Name = "John Doe",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "johndoe",
                };

                var createResult = await _userManager.CreateAsync(user, "@Admin123");
                var addToRoleResult = await _userManager.AddToRoleAsync(user, Roles.Administrators);

                if (!createResult.Succeeded || !addToRoleResult.Succeeded)
                    throw new ApplicationException($"Administrator could not be created: " +
                    $"{string.Join(", ", createResult.Errors.Concat(addToRoleResult.Errors).Select(e => e.Description))}");

                var createResult2 = await _userManager.CreateAsync(author, "@Author123");
                var addToRoleResult2 = await _userManager.AddToRoleAsync(author, Roles.Authors);

                if (!createResult2.Succeeded || !addToRoleResult2.Succeeded)
                    throw new ApplicationException($"Author could not be created: " +
                    $"{string.Join(", ", createResult2.Errors.Concat(addToRoleResult2.Errors).Select(e => e.Description))}");
            }
        }
    }
}
