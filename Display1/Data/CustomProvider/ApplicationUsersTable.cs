//using Display1.Data.Controllers;
using Display1.Models;
using Display1.Data.CustomProvider;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Display1.CustomProvider
{
    public class ApplicationUsersTable
    {
        private readonly AdventureWorks2019Context _context;

        public Guid Rowguid { get; private set; }
        public DateTime ModifiedDate { get; private set; }

        private readonly RoleManager<ApplicationRole> _roleManager;

        public ApplicationUsersTable(AdventureWorks2019Context context, RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }
        public async Task<ApplicationUser> FindByIdAsync(string LoginId)
        {
            var users = from password in _context.Password
                        join email in _context.EmailAddress on password.BusinessEntityId equals email.BusinessEntityId
                        where email.EmailAddress1 == LoginId
                        select new ApplicationUser
                        {
                            PasswordHash = password.PasswordHash,
                            Email = email.EmailAddress1
                        };

            var result = users.SingleOrDefault();

            return result;
        }

         public async Task<ApplicationUser> FindByNameAsync(string email)
        {
            var user = await _context.EmailAddress
                        .Join(_context.Password,
                        email => email.BusinessEntityId,
                        password => password.BusinessEntityId,
                        (email, password) => new ApplicationUser
                        {
                            Email = email.EmailAddress1,
                            PasswordHash = password.PasswordHash
                        })
                        .SingleOrDefaultAsync(u => u.Email == email);

            return user;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            try
            {
                var businessEntity = new BusinessEntity
                {
                    Rowguid = Guid.NewGuid(),
                    ModifiedDate = DateTime.UtcNow
                };

                var newId = _context.BusinessEntity.Add(businessEntity);
                await _context.SaveChangesAsync();



                Person person = new Person
                {
                    BusinessEntityId = newId.Entity.BusinessEntityId,
                    PersonType = "Em",
                    FirstName = "",
                    LastName = "",

                };

                EmailAddress emailAddress = new EmailAddress
                {
                    BusinessEntityId = newId.Entity.BusinessEntityId,
                    EmailAddress1 = user.Email,
                };


                Password password = new Password
                {
                    BusinessEntityId = newId.Entity.BusinessEntityId,
                    PasswordHash = user.PasswordHash,
                    PasswordSalt = "bE3XiWw=",
                };

                await _context.Person.AddAsync(person);
                await _context.EmailAddress.AddAsync(emailAddress);
                await _context.Password.AddAsync(password);
                await _context.SaveChangesAsync();

                user.BusinessEntityId = businessEntity.BusinessEntityId;


                return IdentityResult.Success;

            }
            catch (Exception ex)
            {
                // Handle any exception or error logging here
                return IdentityResult.Failed(new IdentityError { Description = $"Failed to create user: {ex.Message}" });
            }
        }


        public async Task<IdentityResult> DeleteAsync(ApplicationUser user)

        {
            try
            {
                var person = await _context.Person.FindAsync(user.Id);

                if (person == null)
                {
                    return IdentityResult.Failed(new IdentityError { Description = $"User with BusinessEntityId '{user.Id}' not found." });
                }

                _context.Person.Remove(person);
                await _context.SaveChangesAsync();

                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                // Handle any exception or error logging here
                return IdentityResult.Failed(new IdentityError { Description = $"Failed to delete user: {ex.Message}" });
            }
        }

       
    }
}

