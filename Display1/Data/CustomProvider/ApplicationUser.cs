using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Display1.Data.CustomProvider
{
    public class ApplicationUser : IdentityUser
    {
      
        public bool AuthenticationType { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Name { get; set; }
        public string PasswordSalt { get; internal set; }
        public int BusinessEntityId { get; internal set; }
        public string Email { get; internal set; }

        [NotMapped]
        public List<string> Roles { get; set; }
        public bool IsAdmin { get; internal set; }
        // public object Roles { get; internal set; }
    }
}

