using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using System.Threading;
using Display1.Data.CustomProvider;

using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace Display1.CustomProvider
{
    public class CustomUserStore : IUserStore<ApplicationUser>,
        IUserPasswordStore<ApplicationUser>, IUserEmailStore<ApplicationUser>, IUserRoleStore<ApplicationUser>

    {
        private readonly ApplicationUsersTable _usersTable;
      //  private readonly CustomRoleManager _roleManager;

        public CustomUserStore(ApplicationUsersTable usersTable)
        {
            _usersTable = usersTable;
           // _roleManager = roleManager;
        }

        public Task AddToRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task AddToRoleAsync(ApplicationRole user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return await _usersTable.CreateAsync(user);
        }

        public Task<IdentityResult> CreateAsync(ApplicationRole user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(ApplicationRole user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            // Dispose resources if necessary
        }



        public async Task<ApplicationUser> FindByEmailAsync(string Email, CancellationToken cancellationToken)
        {
            return await _usersTable.FindByIdAsync(Email);
        }



        public async Task<ApplicationUser> FindByIdAsync(string Email, CancellationToken cancellationToken)
        {
            return await _usersTable.FindByIdAsync(Email);
        }



        public async Task<ApplicationUser> FindByNameAsync(string userName, CancellationToken cancellationToken)
        {
            return await _usersTable.FindByNameAsync(userName);
        }



        public Task<string> GetEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }



        public async Task<bool> GetEmailConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            //cancellationToken.ThrowIfCancellationRequested();
            //if (user == null)
            //{
            //    throw new ArgumentNullException(nameof(user));
            //}



            //// Retrieve the user from the custom user store
            //var storedUser = await _usersTable.FindByIdAsync(user.Id.ToString());



            //if (storedUser == null)
            //{
            //    // User not found in the custom user store
            //    return false;
            //}



            //// Compare the entered email with the stored email
            //bool isEmailConfirmed = user.Email.Equals(storedUser.Email, StringComparison.OrdinalIgnoreCase);



            return true;
        }



        /*public Task<string> GetNormalizedEmailAsync(Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }*/



        public async Task<string> GetNormalizedEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return user.Email.ToUpper();
            //return await userManager.NormalizeEmail(user);
        }



        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(ApplicationRole user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            return hasher.HashPassword(user, user.Email);
            //return (await _usersTable.FindByIdAsync(user.Email)).PasswordHash;
            //return Task.FromResult(user.PasswordHash);
        }



        /* public async Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken)
         {
             var roleManager = new CustomRoleManager()
             var roles = await roleManager.GetRolesAsync(user);
             return roles;
         }*/


        public async Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            if (user != null)
            {
                if (user.Name == "Admin")
                {
                    return new List<string> { "Admin" };
                }
                else if (user.Name == "User")
                {
                    return new List<string> { "User" };
                }
            }

            return new List<string>();
        }



        public async Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {

            return await Task.FromResult(user.Email);
        }

      

        public async Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.Email);
        }

        public Task<string> GetUserNameAsync(ApplicationRole user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public async Task<bool> IsInRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            var roles = await GetRolesAsync(user, cancellationToken);
            return roles.Contains(roleName);
        }




        public Task RemoveFromRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(ApplicationRole user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(ApplicationUser user, string email, CancellationToken cancellationToken)
        {
            // Set the email address of the user
            user.Email = email;



            // Save the changes to the user
            return Task.CompletedTask;
        }



        public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
        {
            // Set the email confirmation status of the user
            user.EmailConfirmed = confirmed;
            // Save the changes to the user
            return Task.CompletedTask;
        }



        public Task SetNormalizedEmailAsync(ApplicationUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            // Set the normalized email for the user
            user.NormalizedEmail = normalizedEmail;
            return Task.CompletedTask;
        }



        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.FromResult<object>(null);
        }

        public Task SetNormalizedUserNameAsync(ApplicationRole user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
        {
            // throw new NotImplementedException();
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }



        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.CompletedTask;
        }

      

        public Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }



      

    }
}
