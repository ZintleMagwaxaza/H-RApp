using Microsoft.AspNetCore.Identity;

namespace Display1.Data.CustomProvider
{
    public class ApplicationRole : IdentityRole
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}






