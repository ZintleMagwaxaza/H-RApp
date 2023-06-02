using Display1.Data.CustomProvider;
using Display1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Display1.Data.CustomProvider
{
    public class CustomUserManager : UserManager<ApplicationUser>
    {

        public CustomUserManager(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators,
            IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer,
                  errors, services, logger)
        {
        }

        public override Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return Task.FromResult(false);
            }

            bool hasLowercase = false;
            bool hasUppercase = false;
            bool hasDigit = false;

            foreach (char c in password)
            {
                if (char.IsLower(c))
                {
                    hasLowercase = true;
                }
                else if (char.IsUpper(c))
                {
                    hasUppercase = true;
                }
                else if (char.IsDigit(c) || c == '0')
                {
                    hasDigit = true;
                }
            }

            bool isValidPassword = hasLowercase && (hasUppercase || hasDigit);

            return Task.FromResult(isValidPassword);
        }




        /* public override Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
             {
                 var passwordHasher = new PasswordHasher<ApplicationUser>();
                 var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
                 return Task.FromResult(result == PasswordVerificationResult.Success);
             }*/
    }
}





