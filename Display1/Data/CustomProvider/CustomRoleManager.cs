using Display1.Data.CustomProvider;
using Display1.Models;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Security.Claims;

public class CustomRoleManager : RoleManager<ApplicationRole> /*IUserRoleStore<ApplicationRole>*/
{
    private readonly UserManager<ApplicationUser> _userManager;
    string[] _adminEmails = { "rob0@adventure-works.com", "roc0@adventure-works.com", "rus0@adventure-works.com", "ken0@adventure-works.com" };
    public string[] AdminEmails => _adminEmails;
    public CustomRoleManager(IRoleStore<ApplicationRole> store, IEnumerable<IRoleValidator<ApplicationRole>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<ApplicationRole>> logger, UserManager<ApplicationUser> userManager) : base(store, roleValidators, keyNormalizer, errors, logger)
    {
        _userManager = userManager;
    }

    public async Task ConfigureAsync(IApplicationBuilder app, IWebHostEnvironment env)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AdventureWorks2019Context>();

            foreach (string email in _adminEmails)
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    user.IsAdmin = true;
                    await _userManager.UpdateAsync(user);
                }
            }
        }










        /*       public override Task<User> GetUserAsync(ClaimsPrincipal principal)
          {
              var userId = GetUserId(principal);
              return FindByNameAsync(userId);
          }*/
    }

/*
        public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }*/
    

}

   /* public Task AddToRoleAsync(ApplicationRole user, string roleName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task RemoveFromRoleAsync(ApplicationRole user, string roleName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<string>> GetRolesAsync(ApplicationRole user, CancellationToken cancellationToken)
    {
        var usersInRole = await _userManager.GetUsersInRoleAsync(user.Name);
        var roles = usersInRole.SelectMany(u => u.Roles).Distinct().ToList();
        return roles;
    }

    public Task<bool> IsInRoleAsync(ApplicationRole user, string roleName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IList<ApplicationRole>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetUserIdAsync(ApplicationRole user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetUserNameAsync(ApplicationRole user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SetUserNameAsync(ApplicationRole user, string userName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetNormalizedUserNameAsync(ApplicationRole user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SetNormalizedUserNameAsync(ApplicationRole user, string normalizedName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> CreateAsync(ApplicationRole user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> UpdateAsync(ApplicationRole user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> DeleteAsync(ApplicationRole user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationRole> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationRole> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
*/


















































































/*
string[] adminEmails = { "rob0@adventure-works.com", "roc0@adventure-works.com", "rus0@adventure-works.com" };
string adminRole = "Admin";
private readonly UserManager<ApplicationUser> _userManager;

public CustomRoleManager(IRoleStore<ApplicationRole> store, IEnumerable<IRoleValidator<ApplicationRole>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<ApplicationRole>> logger, UserManager<ApplicationUser> userManager) : base(store, roleValidators, keyNormalizer, errors, logger)
{
    _userManager = userManager;
}

public async Task ConfigureAsync(IApplicationBuilder app, IWebHostEnvironment env)
{
    using (var scope = app.ApplicationServices.CreateScope())
    {
        foreach (string email in adminEmails)
        {
            if (await _userManager.FindByEmailAsync(email) == null)
            {
                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email
                };

                var role = await this.FindByNameAsync(adminRole);
                if (role != null)
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                }
            }
        }


    }
}
}*/






/*   if (!await roleManager.RoleExistsAsync("Admin"))
   {
       await roleManager.CreateAsync(new ApplicationRole("Admin"));
   }


   if (!await roleManager.RoleExistsAsync("User"))
   {
       await roleManager.CreateAsync(new ApplicationRole("User"));
   }*/



