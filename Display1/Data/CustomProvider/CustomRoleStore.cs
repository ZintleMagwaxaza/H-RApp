using Display1.CustomProvider;
using Display1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Display1.Data.CustomProvider
{
    public class CustomRoleStore : IRoleStore<ApplicationRole>
    {

   
        

        /*   public async Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken)
           {
               var usersInRole = await userManager.GetUsersInRoleAsync(user.Name);
               var roles = usersInRole.SelectMany(u => u.Roles).Distinct().ToList();
               return roles;
           }*/
        public async Task<IdentityResult> CreateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {

            throw new NotImplementedException();

        }

        public Task<IdentityResult> DeleteAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            if (roleId == "1")
            {
                return new ApplicationRole { Id = 1, Name = "Admin" };
            }
            else if (roleId == "2")
            {
                return new ApplicationRole { Id = 2, Name = "User" };
            }
            else
            {
                return new ApplicationRole { Id = 0, Name = "DefaultRole" }; 
            }
        }



        public async Task<ApplicationRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            if (normalizedRoleName == "admin")
            {
                return new ApplicationRole { Id = 1, Name = "Admin" };
            }
            else
            {
                return new ApplicationRole { Id = 0, Name = "DefaultRole" }; 
            }
        }


        public async Task<string> GetNormalizedRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            return "admin";
        }

        public async Task<string> GetRoleIdAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            if (role.Name == "Admin")
            {
                return "1";
            }
            else if (role.Name == "User")
            {
                return "2";
            }
            else
            {
                return string.Empty; 
            }
        }



        public async Task<string> GetRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            return role.Name;
        }

        public Task SetNormalizedRoleNameAsync(ApplicationRole role, string normalizedName, CancellationToken cancellationToken)
        {

            role.NormalizedName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetRoleNameAsync(ApplicationRole role, string roleName, CancellationToken cancellationToken)
        {

            role.Name = roleName;
            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {

            return Task.FromResult(IdentityResult.Success);
        }

        public void Dispose()
        {
        }
    }
}























       /* public Task AddToRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<ApplicationUser> IUserStore<ApplicationUser>.FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<ApplicationUser> IUserStore<ApplicationUser>.FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }*/

